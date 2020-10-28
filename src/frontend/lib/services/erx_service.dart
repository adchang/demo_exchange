import '../common/grpc_commons.dart';
import '../generated/messages.pb.dart';
import '../generated/service.pbgrpc.dart';

class ErxService {
  static Future<Level2> getLevel2(String ticker) {
    var client = ErxServiceClient(GrpcClientSingleton().client);
    return client.getLevel2(StringMessage()..value = ticker);
  }
}
