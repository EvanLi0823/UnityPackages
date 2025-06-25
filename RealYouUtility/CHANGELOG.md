# 0.0.14
## FIXED
1. IAwaiter中setException时如果task未null，需要new一个新的.

# 0.0.13
## ADD
1. dictionary 辅助函数：直接获取相应类型的value，而不是object类型。
2. log 兼容2018.

# 0.0.12
## MODIFY
1. awaitable 中的reset方法将Error重置
2. 修改comsterUpdate方法，防止dictionary中key找不到.

# 0.0.11
## ADD
1. 支持async IAwaiter<>

## MODIFY
1. 修改async 中的builder里面的setReulst方法
2. 同一个Animator中可以添加多个AnimatorStateEnterAwaiter，此时需要设置AnimatorStateEnterAwaiter.Name 为state的名字.
3. log中添加LogExceptionFormat接口.


# 0.0.10
## ADD
1. log日志ILogOutput，默认使用Unitylog.


## ADD
# 0.0.9
## ADD
1. 添加AsyncCompletionSource.
2. 添加AwaiterError
3. delay支持cancel.

## MODIFY
1. awaitAble的cancel取消IAwaiter限制.
2. customupdate中的uid由uint修改为ulong.


# 0.0.8
## ADD
1. AnimatorStateAwaiterBase 添加OnDestroy,是的在animator在销毁的时候可以正确改变awaiter的状态，从而保证await继续.
2. IAwaiter支持async.

# 0.0.7
## ADD
1.AwaitAble和AwaitAbleMono添加OnCancel
# 0.0.6
## Del
1. 去掉Test目录

# 0.0.5
## ADD
1. awaitOneOf

# 0.0.4
## ADD
1. command 添加了Status属性

## MODIFY
1. Awaiter 的next不再检测IsValid,

# 0.0.3
## ADD
1. command
2. async
3. log
4. pool
5. json
...