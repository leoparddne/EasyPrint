# EasyPrint
<a href="https://www.cnblogs.com/ives/p/EasyPrint.html">参考使用方式</a>  
pc端打印插件  
安装后可以实现打印功能  
支持所有浏览器  
使用url protocol完成  
客户端使用wpf  
支持不预览直接打印功能，提供使用默认打印机直接打印的功能  
同时支持预览与选择打印机的功能  
<a href="https://github.com/leoparddne/EasyPrint/blob/master/EasyPrint/setup.exe">Download</a>

使用方式  
EasyPrint://1&fff  

参数分为两部分使用【&】分隔，第一个参数为是否预览:0:不预览直接打印，会直接调用默认打印机    1:预览，在此模式下会打开客户端，客户端上可以选择直接使用默认打印机打印，或者选择打印机打印  

第二个参数为待打印内容,支持html  
将需要打印的html通过第二个参数发送给打印程序即可  
如果需要打印图片，需要保证打印程序能够正常访问到图片，  
即不能使用相对路径的src，可以根据站点地址使用完整url路径，  
或者直接使用外网地址或是CDN   
