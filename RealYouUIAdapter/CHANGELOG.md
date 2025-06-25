#1.1.0
重写所有的editor，将runtime中的用于editor中的function
完全移出去。
#1.0.3
fixed bug: selector 中使用了相同的GameObject会造成Miss的问题.
#1.0.1
fixed bug：PositionAdapterEditor之前是重写了RectTransformEditor。
PositionAdapterEditor之前是重写了RectTransformEditor是internal
的所以是通过反射来实现的，这样有时候会抛出一些异常。现在为了简单不在重写
RectTransformEditor。