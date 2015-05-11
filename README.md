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

### 5-11:
之前又有很久没有写代码了，罪过罪过。

最近发现dynagon生成mesh也不是很信得过。如果放一个凹凸不平的平面进去，生成的mesh并不好看。所以我想可能是因为delaunay trianglulation的算法对于这种乱七八糟的点阵不太合适，但是要想做成电切模拟，那么乱七八糟的点阵就是跑不了的。所以估计还是要用回voxel，用回voxel的话，deform就是个头痛的事情了呢。但是还是要以做出一个能切的东西为最优先事项吧

现在先再看看另一个代码，quixel
