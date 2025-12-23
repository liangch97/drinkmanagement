# 更新日志 (Changelog)

## [1.0.0] - 2025-12-23

### 新增 (Added)
- 🎉 初始项目发布
- ✨ 实现饮品管理 CRUD API
- ✨ 实现分类管理 CRUD API
- ✨ MySQL 数据库集成（使用 Entity Framework Core）
- ✨ 数据库迁移支持
- ✨ 初始种子数据（4 个分类，6 种饮品）
- ✨ RESTful API 设计
- ✨ 输入验证（DataAnnotations）
- ✨ CORS 支持
- 📝 完整的项目文档（README、API 测试指南、部署指南）
- 📝 数据库初始化 SQL 脚本
- 📝 项目总结文档

### 安全 (Security)
- 🔒 使用专用数据库用户代替 root
- 🔒 强密码建议
- 🔒 环境变量支持
- 🔒 SQL 注入防护（EF Core 参数化查询）
- 🔒 空值和空白字符串验证
- 🔒 通过 CodeQL 安全扫描

### 功能特性
#### 饮品管理
- GET /api/drinks - 获取所有饮品
- GET /api/drinks/{id} - 获取单个饮品
- GET /api/drinks/category/{categoryId} - 按分类获取饮品
- POST /api/drinks - 创建新饮品
- PUT /api/drinks/{id} - 更新饮品
- DELETE /api/drinks/{id} - 删除饮品

#### 分类管理
- GET /api/categories - 获取所有分类
- GET /api/categories/{id} - 获取单个分类
- POST /api/categories - 创建新分类
- PUT /api/categories/{id} - 更新分类
- DELETE /api/categories/{id} - 删除分类（带约束检查）

### 技术细节
- .NET 10.0
- ASP.NET Core Web API
- Entity Framework Core 8.0
- Pomelo.EntityFrameworkCore.MySql 8.0
- MySQL 8.0+

### 文档
- README.md - 项目介绍和快速入门
- API_TESTING.md - API 测试指南
- DEPLOYMENT.md - 部署指南
- PROJECT_SUMMARY.md - 项目总结
- database_init.sql - 数据库初始化脚本

---

## 计划功能 (Planned Features)

### [2.0.0] - 未来版本
- 🔐 JWT 身份验证和授权
- 📊 订单管理系统
- 💳 支付集成
- 📱 图片上传功能
- 🔍 高级搜索和过滤
- 📄 分页功能
- 📈 销售报表
- ⚡ Redis 缓存
- 🧪 单元测试和集成测试
- 📱 移动端 API 优化

---

## 贡献指南

如果您想为项目做出贡献，请：
1. Fork 项目
2. 创建特性分支 (`git checkout -b feature/AmazingFeature`)
3. 提交更改 (`git commit -m 'Add some AmazingFeature'`)
4. 推送到分支 (`git push origin feature/AmazingFeature`)
5. 打开 Pull Request

## 问题反馈

如发现 bug 或有功能建议，请在 GitHub Issues 中提交。

---

**最后更新**: 2025-12-23  
**维护者**: liangch97  
**许可证**: MIT License
