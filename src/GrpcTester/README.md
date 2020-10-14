# Self signed-cert

https://andrewlock.net/creating-and-trusting-a-self-signed-certificate-on-linux-for-use-in-kestrel-and-asp-net-core/

After running gen.sh, do:

    #Install the cert utils
    sudo apt install libnss3-tools
    # Trust the certificate for SSL 
    pk12util -d sql:$HOME/.pki/nssdb -i localhost.pfx
    # Trust a self-signed server certificate
    certutil -d sql:$HOME/.pki/nssdb -A -t "P,," -n 'dev cert' -i localhost.crt
