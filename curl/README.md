# Using Curl for Server Sent Events

Verify you have a version of curl which supports HTTP/2.  Tested with curl version 7.54.0 on Mac:

```shell
$ curl --version

curl 7.54.0 (x86_64-apple-darwin18.0) libcurl/7.54.0 LibreSSL/2.6.5 zlib/1.2.11 nghttp2/1.24.1
Protocols: dict file ftp ftps gopher http https imap imaps ldap ldaps pop3 pop3s rtsp smb smbs smtp smtps telnet tftp
Features: AsynchDNS IPv6 Largefile GSS-API Kerberos SPNEGO NTLM NTLM_WB SSL libz HTTP2 UnixSockets HTTPS-proxy
```


Connecting to SSE stream:

```shell
$ curl -N --http2 -f -H "Accept:text/event-stream" \<Server-Sent Events URL\>
```


