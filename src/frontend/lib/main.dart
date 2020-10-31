import 'package:flutter/material.dart';

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
  Level2 _level2;

  @override
  void initState() {
    _level2 = Level2()
      ..asks.add(Level2Quote()
        ..price = 0
        ..quantity = 0)
      ..bids.add(Level2Quote()
        ..price = 0
        ..quantity = 0);
    super.initState();
  }

  Future<void> _getLevel2Streams() async {
    ErxService.getLevel2Streams("SPY").listen((Level2 response) {
      setState(() {
        _level2 = response;
      });
    });
  }

  Widget _buildLevel2(BuildContext context, String type) {
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
      body: Center(
        child: Container(
          child: Row(
            children: <Widget>[
              _buildLevel2(context, "Bids"),
              _buildLevel2(context, "Asks"),
            ],
          ),
        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () async => _getLevel2Streams(),
        tooltip: 'Get Level 2',
        child: Icon(Icons.request_quote_rounded),
      ),
    );
  }
}
