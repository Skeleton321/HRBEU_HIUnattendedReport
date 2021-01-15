## HRBEU_HIUnattendedReport

一个用于哈尔滨工程大学平安行动的自动化工具。

代码写得烂，望dalao轻喷。

    已实现的功能:
        1. 打卡
        2. 截图
        3. 发送截图至指定的群

### 运行要求
- .NET Framework 4.5
- (不需要截图时可选) Chromium 89.0.4388.0

### 工作机制

打卡部分使用了`HttpClient`和[`Newtonsoft.Json`](https://github.com/JamesNK/Newtonsoft.Json)  
截图部分使用了`Selenium`，浏览器使用了`Chromium`  
发送部分使用了[`Mirai`](https://github.com/mamoe/mirai/)

截图时并没有设置Chromium为headerless，主要是为了Selenium出现问题时也可以手动截图。

### 使用方法

#### 1. 自行编译
第一步: 将项目clone至本地:
`git clone https://github.com/Skeleton321/HRBEU_HIUnattendedReport.git`

第二步: 使用Visual Studio 2019打开本项目 (`HRBEUHIUnattended.sln`)

第三步: 在 "解决方案管理器" 中右键选择 UnattendedReportProxy，点击 "生成"

第四步: 见下

#### 2. 直接使用二进制文件

如果没有Visual Studio或不会使用，也可以使用打包好的二进制文件，其中包含了所有的可执行文件、dll，和一个Chromium。

一般来说，最常用的是`UnattendedReportProxy.exe`，它相比于`HRBEU_HIUnattendedReport.exe`来说更符合大众的使用习惯，同时有定时功能。

`UnattendedReportProxy.exe`运行后会要求用户输入学号 密码 签到时间 要发送图片的群。按操作完成后即可进行自动签到。  
`HRBEU_HIUnattendedReport.exe`主要用于命令行，功能比前者更多，但是不能直接双击运行。命令行中输入  `HRBEU_HIUnattendedReport.exe -h`可以查看所有用法。

### 注意事项

1. 第一次运行可能会出现无法签到的情况，建议尽量先运行一次，防止签到时出问题。
2. 测试用例较少，无法覆盖到所有可能的情况，如果出现问题请发issue。
3. `UnattendedReportProxy.exe`有重试机制，理论上可以一直运行，但是本人并未做稳定性测试，建议每天起床过后关闭，睡觉之前再打开。电脑不能关。
4. 有问题请发issue，如果尝试自己改代码，你将会看到: 鬼才变量\函数名; 高血压函数(指一个函数一百多行); 只有马克思才看得懂的代码。
5. 如果有意见也可以发issue。