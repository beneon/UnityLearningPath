# UnityLearningPath
my own log for unity scripting.

每天坚持编程15分钟，注意养成这个好习惯。目前经常要用Unity，先从Unity开始写吧
1.Unity之前先还是要学学C#的……先从C#开始吧

###4-10:
最近弄了一下三次样条插值曲线，现在把做好的代码放上来了

- 里面TDMA的算法是来自[这里](http://www.cnblogs.com/xpvincent/archive/2013/01/25/2877411.html)
- 三次样条曲线的绘制因为是在unity里面比起其他人的有些简化，虽然还是有点繁杂的样子
- 目前测试了一下还没有看到bug。

```
CubicInterp CI=new CubicInterp(p,10);
outV=CI.CubicCalc();
```

- p是控制点的坐标集，比如你已经有6个点，把这6个点放进数组
10是每一段分成多少个小段，越大曲线越平滑
- CI.CubicCalc()返回的就是扩增以后的点集
