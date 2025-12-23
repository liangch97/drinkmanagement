# 饮品管理系统 (Drink Management System)

这是一个基于 ASP.NET Core 和 MySQL 开发的饮品管理系统。

## 功能特性

- 饮品管理（增删改查）
- 分类管理（增删改查）
- 库存管理
- RESTful API 接口
- MySQL 数据库支持

## 技术栈

- .NET 10.0
- ASP.NET Core Web API
- Entity Framework Core 8.0
- MySQL 数据库
- Pomelo.EntityFrameworkCore.MySql

## 前置要求

- .NET SDK 10.0 或更高版本
- MySQL 8.0 或更高版本

## 安装步骤

### 1. 克隆项目

```bash
git clone https://github.com/liangch97/drinkmanagement.git
cd drinkmanagement/DrinkManagement
```

### 2. 配置数据库连接

**重要安全建议：** 创建专用数据库用户而不是使用 root 用户

```sql
-- 连接到 MySQL
mysql -u root -p

-- 创建数据库
CREATE DATABASE drinkmanagement CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- 创建专用用户
CREATE USER 'drinkuser'@'localhost' IDENTIFIED BY 'YourSecurePassword123!';

-- 授予权限
GRANT ALL PRIVILEGES ON drinkmanagement.* TO 'drinkuser'@'localhost';
FLUSH PRIVILEGES;
```

编辑 `DrinkManagement/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=drinkmanagement;User=drinkuser;Password=YourSecurePassword123!;"
  }
}
```

**注意：** 生产环境建议使用环境变量存储数据库连接字符串，而不是硬编码在配置文件中。

### 3. 创建数据库

在 MySQL 中创建数据库：

```sql
CREATE DATABASE drinkmanagement CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
```

### 4. 运行数据库迁移

```bash
# 安装 EF Core 工具（如果还没安装）
dotnet tool install --global dotnet-ef

# 创建迁移
dotnet ef migrations add InitialCreate

# 更新数据库
dotnet ef database update
```

### 5. 运行应用

```bash
dotnet run
```

应用将在 `http://localhost:5000` 或 `https://localhost:5001` 启动。

## API 端点

### 饮品管理 (Drinks)

- `GET /api/drinks` - 获取所有饮品
- `GET /api/drinks/{id}` - 获取指定饮品
- `GET /api/drinks/category/{categoryId}` - 获取指定分类的饮品
- `POST /api/drinks` - 创建新饮品
- `PUT /api/drinks/{id}` - 更新饮品
- `DELETE /api/drinks/{id}` - 删除饮品

### 分类管理 (Categories)

- `GET /api/categories` - 获取所有分类
- `GET /api/categories/{id}` - 获取指定分类
- `POST /api/categories` - 创建新分类
- `PUT /api/categories/{id}` - 更新分类
- `DELETE /api/categories/{id}` - 删除分类

## API 示例

### 创建饮品

```bash
curl -X POST http://localhost:5000/api/drinks \
  -H "Content-Type: application/json" \
  -d '{
    "name": "冰美式",
    "description": "冰镇美式咖啡",
    "price": 16.00,
    "categoryId": 2,
    "stock": 50
  }'
```

### 获取所有饮品

```bash
curl http://localhost:5000/api/drinks
```

### 更新饮品

```bash
curl -X PUT http://localhost:5000/api/drinks/1 \
  -H "Content-Type: application/json" \
  -d '{
    "price": 9.00,
    "stock": 120
  }'
```

### 删除饮品

```bash
curl -X DELETE http://localhost:5000/api/drinks/1
```

## 数据库架构

### Categories 表
- Id (int, 主键)
- Name (string, 50)
- Description (string, 200)
- CreatedAt (datetime)

### Drinks 表
- Id (int, 主键)
- Name (string, 100)
- Description (string, 500)
- Price (decimal, 10,2)
- CategoryId (int, 外键)
- Stock (int)
- ImageUrl (string, 500)
- CreatedAt (datetime)
- UpdatedAt (datetime)

## 初始数据

系统会自动创建以下初始数据：

**分类:**
- 茶饮
- 咖啡
- 果汁
- 奶茶

**饮品:**
- 绿茶 (8元)
- 红茶 (8元)
- 美式咖啡 (15元)
- 拿铁 (18元)
- 橙汁 (12元)
- 珍珠奶茶 (10元)

## 开发说明

### 添加新的迁移

```bash
dotnet ef migrations add MigrationName
dotnet ef database update
```

### 回滚迁移

```bash
dotnet ef database update PreviousMigrationName
```

### 删除迁移

```bash
dotnet ef migrations remove
```

## 许可证

MIT License

## 联系方式

如有问题，请联系：liangch97@mail2.sysu.edu.cn
