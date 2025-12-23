# 部署指南 (Deployment Guide)

本指南将帮助您在不同环境中部署饮品管理系统。

## 开发环境部署

### 前置条件

1. 安装 .NET 10.0 SDK 或更高版本
2. 安装 MySQL 8.0 或更高版本
3. (可选) 安装 Visual Studio 2022 或 VS Code

### 步骤

1. **克隆项目**
   ```bash
   git clone https://github.com/liangch97/drinkmanagement.git
   cd drinkmanagement
   ```

2. **创建数据库**
   
   选项 A: 使用 SQL 脚本
   ```bash
   mysql -u root -p < database_init.sql
   ```
   
   选项 B: 使用 Entity Framework 迁移
   ```bash
   cd DrinkManagement
   dotnet ef database update
   ```

3. **配置连接字符串**
   
   编辑 `DrinkManagement/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Port=3306;Database=drinkmanagement;User=your_user;Password=your_password;"
     }
   }
   ```

4. **运行应用**
   ```bash
   cd DrinkManagement
   dotnet run
   ```

5. **访问 API**
   - HTTP: http://localhost:5000
   - HTTPS: https://localhost:5001
   - Swagger (开发环境): http://localhost:5000/openapi/v1.json

## 生产环境部署

### 使用 Docker 部署 (推荐)

1. **创建 Dockerfile**
   
   在项目根目录创建 `Dockerfile`:
   ```dockerfile
   FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
   WORKDIR /app
   EXPOSE 80
   EXPOSE 443

   FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
   WORKDIR /src
   COPY ["DrinkManagement/DrinkManagement.csproj", "DrinkManagement/"]
   RUN dotnet restore "DrinkManagement/DrinkManagement.csproj"
   COPY . .
   WORKDIR "/src/DrinkManagement"
   RUN dotnet build "DrinkManagement.csproj" -c Release -o /app/build

   FROM build AS publish
   RUN dotnet publish "DrinkManagement.csproj" -c Release -o /app/publish

   FROM base AS final
   WORKDIR /app
   COPY --from=publish /app/publish .
   ENTRYPOINT ["dotnet", "DrinkManagement.dll"]
   ```

2. **创建 docker-compose.yml**
   ```yaml
   version: '3.8'
   services:
     mysql:
       image: mysql:8.0
       container_name: drinkmanagement_mysql
       environment:
         MYSQL_ROOT_PASSWORD: root_password
         MYSQL_DATABASE: drinkmanagement
         MYSQL_USER: dbuser
         MYSQL_PASSWORD: dbpassword
       ports:
         - "3306:3306"
       volumes:
         - mysql_data:/var/lib/mysql
       networks:
         - drinkmanagement_network

     api:
       build: .
       container_name: drinkmanagement_api
       environment:
         - ASPNETCORE_ENVIRONMENT=Production
         - ConnectionStrings__DefaultConnection=Server=mysql;Port=3306;Database=drinkmanagement;User=dbuser;Password=dbpassword;
       ports:
         - "5000:80"
       depends_on:
         - mysql
       networks:
         - drinkmanagement_network

   volumes:
     mysql_data:

   networks:
     drinkmanagement_network:
       driver: bridge
   ```

3. **启动服务**
   ```bash
   docker-compose up -d
   ```

4. **执行数据库迁移**
   ```bash
   docker exec -it drinkmanagement_api dotnet ef database update
   ```

### 直接部署到 Linux 服务器

1. **安装依赖**
   ```bash
   # Ubuntu/Debian
   sudo apt-get update
   sudo apt-get install -y apt-transport-https
   wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb
   sudo dpkg -i packages-microsoft-prod.deb
   sudo apt-get update
   sudo apt-get install -y dotnet-sdk-10.0
   
   # 安装 MySQL
   sudo apt-get install -y mysql-server
   ```

2. **发布应用**
   ```bash
   cd DrinkManagement
   dotnet publish -c Release -o /var/www/drinkmanagement
   ```

3. **配置 systemd 服务**
   
   创建 `/etc/systemd/system/drinkmanagement.service`:
   ```ini
   [Unit]
   Description=Drink Management API
   After=network.target

   [Service]
   WorkingDirectory=/var/www/drinkmanagement
   ExecStart=/usr/bin/dotnet /var/www/drinkmanagement/DrinkManagement.dll
   Restart=always
   RestartSec=10
   KillSignal=SIGINT
   SyslogIdentifier=drinkmanagement
   User=www-data
   Environment=ASPNETCORE_ENVIRONMENT=Production
   Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

   [Install]
   WantedBy=multi-user.target
   ```

4. **启动服务**
   ```bash
   sudo systemctl daemon-reload
   sudo systemctl enable drinkmanagement
   sudo systemctl start drinkmanagement
   sudo systemctl status drinkmanagement
   ```

5. **配置 Nginx 反向代理**
   
   创建 `/etc/nginx/sites-available/drinkmanagement`:
   ```nginx
   server {
       listen 80;
       server_name your_domain.com;

       location / {
           proxy_pass http://localhost:5000;
           proxy_http_version 1.1;
           proxy_set_header Upgrade $http_upgrade;
           proxy_set_header Connection keep-alive;
           proxy_set_header Host $host;
           proxy_cache_bypass $http_upgrade;
           proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
           proxy_set_header X-Forwarded-Proto $scheme;
       }
   }
   ```
   
   ```bash
   sudo ln -s /etc/nginx/sites-available/drinkmanagement /etc/nginx/sites-enabled/
   sudo nginx -t
   sudo systemctl reload nginx
   ```

## 数据库备份和恢复

### 备份数据库
```bash
mysqldump -u root -p drinkmanagement > backup_$(date +%Y%m%d_%H%M%S).sql
```

### 恢复数据库
```bash
mysql -u root -p drinkmanagement < backup_20251223_123456.sql
```

## 环境变量配置

对于生产环境，建议使用环境变量而不是 appsettings.json：

```bash
export ConnectionStrings__DefaultConnection="Server=localhost;Port=3306;Database=drinkmanagement;User=dbuser;Password=dbpassword;"
export ASPNETCORE_ENVIRONMENT=Production
```

## 性能优化建议

1. **启用响应缓存**
2. **配置数据库连接池**
3. **使用 Redis 缓存热点数据**
4. **启用 gzip 压缩**
5. **配置 CDN 加速静态资源**

## 监控和日志

### 日志位置
- 开发环境: Console
- 生产环境: `/var/log/drinkmanagement/`

### 推荐监控工具
- Application Insights
- Prometheus + Grafana
- ELK Stack

## 安全建议

1. 修改默认数据库密码
2. 配置 HTTPS 证书 (Let's Encrypt)
3. 限制 API 访问频率
4. 实施 JWT 认证授权
5. 启用 SQL 注入防护
6. 配置防火墙规则

## 故障排查

### 数据库连接失败
```bash
# 检查 MySQL 状态
sudo systemctl status mysql

# 测试连接
mysql -h localhost -u root -p
```

### 应用无法启动
```bash
# 查看日志
sudo journalctl -u drinkmanagement -f

# 检查端口占用
sudo netstat -tulpn | grep 5000
```

### 性能问题
```bash
# 检查数据库查询性能
mysql> SHOW PROCESSLIST;
mysql> EXPLAIN SELECT * FROM Drinks;

# 查看应用资源使用
top
htop
```

## 联系支持

如遇到问题，请联系：liangch97@mail2.sysu.edu.cn
