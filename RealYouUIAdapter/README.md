#背景
正常情况下的手机适配我们是通过unity中的rect transform来做适配，
但是在我们项目中出现了下面的场景
  * 不同分辨率下使用不同的Image
  * 不同分辨率下GameObject的位置不一样
  * 不同分辨率下GameObject显示与否不一样
  * 不同分辨率下显示的内容不一样
  
#提供的功能

 * Display 用于控制是否显示
 * Selector 用于选择显示不同的GameObject
 * Image 用于加载不同的Image
 * Postion 用于布局不同的Position