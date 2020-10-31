import 'package:grpc/grpc.dart';

class GrpcClientSingleton {
  ClientChannel client;
  static final GrpcClientSingleton _singleton =
      new GrpcClientSingleton._internal();

  factory GrpcClientSingleton() => _singleton;

  GrpcClientSingleton._internal() {
    client = ClientChannel("hela.codex.in.th",
        port: 8080,
        options: ChannelOptions(
          //TODO: Change to secure with server certificates
          credentials: ChannelCredentials.insecure(),
          idleTimeout: Duration(minutes: 1),
        ));
  }
}
/*
DefaultAssetBundle.of(context).load("assets/grpc.crt").then((bytes) => setState(() {
      client = GrpcServicesClient(
          ClientChannel('todoworld.servicestack.net', port:50051,
              options:ChannelOptions(credentials: ChannelCredentials.secure(
                  certificates: bytes.buffer.asUint8List(),
                  authority: 'todoworld.servicestack.net'
              ))));
    }));
*/
