import 'package:flutter/material.dart';

import 'graph.dart';
import 'generated/messages.pb.dart';
import 'services/erx_service.dart';

void main() {
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Hello 8cuz',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: MyHomePage(title: 'DemoExchange'),
    );
  }
}

class MyHomePage extends StatefulWidget {
  final String title;

  MyHomePage({Key key, this.title}) : super(key: key);

  @override
  _MyHomePageState createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  ChartData _chartData;
  Level2 _level2;

  @override
  void initState() {
    super.initState();
  }

  Future<void> _getStreams() async {
    ErxService.getHistoricalPriceStreams("ERX")
        .listen((HistoricalPrice response) {
      List<PriceData> data =
          (_chartData == null) ? [] : List.from(_chartData.prices);
      PriceData current = PriceData(response.timestamp, response.open,
          response.close, response.high, response.low, response.volume);
      if (data.isEmpty) {
        data.add(current);
      } else {
        if (data[data.length - 1].time == current.time) {
          data[data.length - 1] = current;
        } else {
          data.add(current);
          if (data.length == 51) {
            data.removeAt(0);
          }
        }
      }
      setState(() {
        _chartData = ChartData(data);
      });
    });
    ErxService.getLevel2Streams("ERX").listen((Level2 response) {
      setState(() {
        _level2 = response;
      });
    });
  }

  Widget _buildLevel2(BuildContext context, String type) {
    if (_level2 == null || _level2.bids.isEmpty) {
      return Text("");
    }
    List<Level2Quote> quotes = (type == "Bids") ? _level2.bids : _level2.asks;
    return Expanded(
      flex: 5,
      child: Column(children: <Widget>[
        Text(type),
        Expanded(
            child: ListView.builder(
          itemCount: quotes.length,
          itemBuilder: (context, index) {
            return ListTile(
              title: Text('${quotes[index].quantity} @ ${quotes[index].price}'),
            );
          },
        )),
      ]),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(widget.title),
      ),
      body: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: <Widget>[
          SizedBox(
            height: 160,
            child: CustomPaint(
              size: Size.infinite,
              painter: CandlestickPainter(_chartData),
            ),
          ),
          SizedBox(height: 5),
          SizedBox(
            height: 30,
            child: CustomPaint(
              size: Size.infinite,
              painter: VolumePainter(_chartData),
            ),
          ),
          SizedBox(height: 10),
          SizedBox(
            height: 260,
            child: Row(
              children: <Widget>[
                _buildLevel2(context, "Bids"),
                _buildLevel2(context, "Asks"),
              ],
            ),
          ),
        ],
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () async => _getStreams(),
        tooltip: 'Get Live Data',
        child: Icon(Icons.request_quote_rounded),
      ),
    );
  }
}
