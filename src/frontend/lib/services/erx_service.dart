import '../common/grpc_commons.dart';
import '../generated/messages.pb.dart';
import '../generated/service.pbgrpc.dart';

class ErxService {
  static Future<Level2> getLevel2(String ticker) {
    var client = ErxServiceClient(GrpcClientSingleton().client);
    return client.getLevel2(StringMessage()..value = ticker);
  }

  static Stream<Level2> getLevel2Streams(String ticker) async* {
    var client = ErxServiceClient(GrpcClientSingleton().client);
    await for (Level2 level2
        in client.getLevel2Streams(StringMessage()..value = ticker)) {
      yield level2;
    }
  }

  static Stream<HistoricalPrice> getHistoricalPriceStreams(
      String ticker) async* {
    var client = ErxServiceClient(GrpcClientSingleton().client);
    await for (HistoricalPrice price
        in client.getHistoricalPriceStreams(HistoricalPriceRequest()
          ..ticker = ticker
          ..type = PriceType.PRICE_TYPE_10_SECONDS)) {
      yield price;
    }
  }
}

/*
Future<pb.MovieMessage> send(pb.MovieMessage query) {

  var completer = new Completer<pb.MovieMessage>();
  var uri = Uri.parse("http://localhost:8080/public/data/");

  var request = new HttpRequest()
  ..open("POST", uri.toString(), async: true)
    ..overrideMimeType("application/x-google-protobuf")
    ..setRequestHeader("Accept", "application/x-google-protobuf")
    ..setRequestHeader("Content-Type", "application/x-google-protobuf")
    ..responseType = "arraybuffer"
    ..withCredentials = true // seems to be necessary so that cookies are sent
    ..onError.listen((e) {
      completer.completeError(e);
    })
    ..onProgress.listen((e){},
        onError:(e) => _logger.severe("Error: " + e.errorMessage));

    request.onReadyStateChange.listen((e){},
        onError: (e) => _logger.severe("OnReadyStateChange.OnError: " + e.toString())
        );

    request.onLoad.listen((ProgressEvent e) {
      if ((request.status >= 200 && request.status < 300) ||
          request.status == 0 || request.status == 304) {

        List<int> buffer = new Uint8List.view(request.response);
        var response = new pb.MovieMessage.fromBuffer(buffer);
        response.errors.forEach((pb.Error e) => _logger.severe("Error: " + e.errorMessage));

        completer.complete(response);
      } else {
        completer.completeError(e);
      }
    });
*/
