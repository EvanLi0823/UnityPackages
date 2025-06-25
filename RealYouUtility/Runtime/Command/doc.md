## summary
### command
1. command 基于async设计
2. command的执行依赖于queue

### queue

1. 当queue的并发数为1的时候，意味着queue为阻塞队列
2. 当queue的并发数为max的时候，意味着queue为非阻塞

## 范例

```c#
public IEnumerator CreateCommand()
{
    SimpleCommand command = new SimpleCommand();
    yield return command.Start();
}

public async void CreateCommand()
{
    SimpleCommand command = new SimpleCommand();
    await command.Start();
}

```
## TODO
1. command 优先级.

## API
### Start
```c#
public IAsyncOperation Start()
```
开始执行command.

### Cancel