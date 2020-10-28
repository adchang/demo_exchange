import 'package:flutter/material.dart';

import 'generated/messages.pb.dart';
import 'services/erx_service.dart';

void main() {
  runApp(MyApp());
}

alertDialog(BuildContext context, String message) {
  Widget ok = FlatButton(
    child: Text("OK"),
    onPressed: () {
      Navigator.of(context).pop();
    },
  );
  showDialog(
    context: context,
    builder: (BuildContext context) {
      return AlertDialog(
        title: Text("Pushed"),
        content: Text(message),
        actions: [
          ok,
        ],
      );
    },
  );
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
  MyHomePage({Key key, this.title}) : super(key: key);

  final String title;

  @override
  _MyHomePageState createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  Level2 _level2;

  @override
  void initState() {
    super.initState();
  }

  Future<void> _getLevel2(BuildContext context) async {
    ErxService.getLevel2("SPY").then((Level2 val) {
      setState(() {
        _level2 = val;
      });
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(widget.title),
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            Text(
              _level2.toString(),
            ),
          ],
        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () async => _getLevel2(context),
        tooltip: 'Get Level 2',
        child: Icon(Icons.request_quote_rounded),
      ),
    );
  }
}
