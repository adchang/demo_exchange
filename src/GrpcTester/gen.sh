openssl req -config localhost.conf -new -x509 -sha256 -newkey rsa:2048 -nodes \
    -keyout localhost.key -days 3650 -out localhost.crt
openssl pkcs12 -export -out localhost.pfx -inkey localhost.key -in localhost.crt
