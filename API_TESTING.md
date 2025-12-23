# API 测试指南

## 测试前准备

1. 确保 MySQL 数据库已启动
2. 执行 `database_init.sql` 脚本初始化数据库
3. 启动应用程序: `dotnet run`

## 使用 curl 测试 API

### 分类管理 API

#### 1. 获取所有分类
```bash
curl -X GET http://localhost:5000/api/categories
```

#### 2. 获取单个分类
```bash
curl -X GET http://localhost:5000/api/categories/1
```

#### 3. 创建新分类
```bash
curl -X POST http://localhost:5000/api/categories \
  -H "Content-Type: application/json" \
  -d '{
    "name": "冰品",
    "description": "各种冰品饮料"
  }'
```

#### 4. 更新分类
```bash
curl -X PUT http://localhost:5000/api/categories/1 \
  -H "Content-Type: application/json" \
  -d '{
    "description": "更新后的描述"
  }'
```

#### 5. 删除分类
```bash
curl -X DELETE http://localhost:5000/api/categories/5
```

### 饮品管理 API

#### 1. 获取所有饮品
```bash
curl -X GET http://localhost:5000/api/drinks
```

#### 2. 获取单个饮品
```bash
curl -X GET http://localhost:5000/api/drinks/1
```

#### 3. 按分类获取饮品
```bash
curl -X GET http://localhost:5000/api/drinks/category/1
```

#### 4. 创建新饮品
```bash
curl -X POST http://localhost:5000/api/drinks \
  -H "Content-Type: application/json" \
  -d '{
    "name": "柠檬水",
    "description": "清爽柠檬水",
    "price": 6.50,
    "categoryId": 3,
    "stock": 150,
    "imageUrl": "https://example.com/lemon.jpg"
  }'
```

#### 5. 更新饮品
```bash
curl -X PUT http://localhost:5000/api/drinks/1 \
  -H "Content-Type: application/json" \
  -d '{
    "price": 9.50,
    "stock": 200
  }'
```

#### 6. 删除饮品
```bash
curl -X DELETE http://localhost:5000/api/drinks/7
```

## 使用 Postman 测试

### 导入集合

您可以在 Postman 中创建一个新的集合，并添加以下请求：

1. **Get All Drinks**
   - Method: GET
   - URL: `http://localhost:5000/api/drinks`

2. **Get Drink by ID**
   - Method: GET
   - URL: `http://localhost:5000/api/drinks/1`

3. **Create Drink**
   - Method: POST
   - URL: `http://localhost:5000/api/drinks`
   - Headers: `Content-Type: application/json`
   - Body (raw JSON):
   ```json
   {
     "name": "冰美式",
     "description": "冰镇美式咖啡",
     "price": 16.00,
     "categoryId": 2,
     "stock": 50
   }
   ```

4. **Update Drink**
   - Method: PUT
   - URL: `http://localhost:5000/api/drinks/1`
   - Headers: `Content-Type: application/json`
   - Body (raw JSON):
   ```json
   {
     "price": 9.00,
     "stock": 120
   }
   ```

5. **Delete Drink**
   - Method: DELETE
   - URL: `http://localhost:5000/api/drinks/1`

## 预期响应示例

### 成功获取饮品列表
```json
[
  {
    "id": 1,
    "name": "绿茶",
    "description": "清新绿茶",
    "price": 8.00,
    "categoryId": 1,
    "categoryName": "茶饮",
    "stock": 100,
    "imageUrl": null,
    "createdAt": "2025-12-23T03:00:00Z",
    "updatedAt": "2025-12-23T03:00:00Z"
  }
]
```

### 成功创建饮品
```json
{
  "id": 7,
  "name": "柠檬水",
  "description": "清爽柠檬水",
  "price": 6.50,
  "categoryId": 3,
  "categoryName": "果汁",
  "stock": 150,
  "imageUrl": "https://example.com/lemon.jpg",
  "createdAt": "2025-12-23T03:30:00Z",
  "updatedAt": "2025-12-23T03:30:00Z"
}
```

### 错误响应示例

#### 饮品未找到 (404)
```json
{
  "message": "饮品未找到"
}
```

#### 分类不存在 (400)
```json
{
  "message": "分类不存在"
}
```

## 常见问题

### 1. 连接数据库失败
- 检查 MySQL 服务是否启动
- 检查 `appsettings.json` 中的连接字符串
- 验证数据库用户名和密码

### 2. 端口被占用
- 修改 `Properties/launchSettings.json` 中的端口配置
- 或使用命令: `dotnet run --urls="http://localhost:5100"`

### 3. CORS 错误
- 本项目已配置允许所有来源的 CORS 策略
- 如需更严格的策略，修改 `Program.cs` 中的 CORS 配置
