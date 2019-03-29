# Using Go for Server Sent Events

Tested with Go 1.12.1 on Mac, installed with brew:

```shell
$ brew install go
```

Prepare Go environment by grabbing Event Source package:

```shell
$ go get github.com/donovanhide/eventsource
```

Run:
```shell
$ go run sse.go <URL>
```

