# cyber psychosis
## 项目基本说明-程序组
引擎：[Unity 2021.3.30.f1](https://unity.com/releases/editor/whats-new/2021.3.30) (2021 最新lts版本，推荐使用Unity Hub安装)   
AI接入：尝试ChatGPT （Inworld AI无中文  
项目结构：  
- data  存放配置的data（策划）
- doc   存放文档（策划、文案）
- asset 美术资源 （美术）
- src   项目工程存放（程序、美术）
- tools 测试功能时写的一些工具等（垂直组）

![](asset\Markdown/001.png)

版本管理：[Git](https://git-scm.com/downloads) （gitee）

## git使用说明
```bash
git clone https://gitee.com/beta-cat_1/cyber-psychosis.git
```
拉取项目后，在Unity中打开项目src/cyber-psychosis

每次提交前请先拉取最新版本，避免冲突
```bash
git pull
```
提交步骤说明
```bash
git add .
git commit -m "Update README.md"
git push 
```
第一次提交时需要登录gitee账号，之后会自动保存