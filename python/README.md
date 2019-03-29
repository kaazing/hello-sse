# Using Python for Server Sent Events

Tested with Python 3.7.2 on Mac

```shell
$ brew install python
```

Prepare Python environment by grabbing required packages.  Recommend using virtualenv to isolate from system libraries:

```shell
$ pip install requests
$ pip install hyper
$ pip install sseclient-py
```

Run:
```shell
$ ./sse.py <Server-Sent Events URL>
```