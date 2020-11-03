import 'package:fixnum/fixnum.dart';
import 'package:flutter/material.dart';

class CandlestickPainter extends CustomPainter {
  final ChartData chartData;
  final Paint _wickPaint;
  final Paint _gainPaint;
  final Paint _lossPaint;
  final double _wickWidth = 1.0;
  final double _candleWidth = 5.0;

  CandlestickPainter(this.chartData)
      : _wickPaint = Paint()..color = Colors.black,
        _gainPaint = Paint()..color = Colors.green,
        _lossPaint = Paint()..color = Colors.red;

  @override
  void paint(Canvas canvas, Size size) {
    if (chartData == null) {
      return;
    }

    List<Candlestick> candlesticks = _generateCandlesticks(size);
    for (Candlestick candlestick in candlesticks) {
      canvas.drawRect(
          Rect.fromLTRB(
              candlestick.centerX - (_wickWidth / 2),
              size.height - candlestick.wickHighY,
              candlestick.centerX + (_wickWidth / 2),
              size.height - candlestick.wickLowY),
          _wickPaint);
      canvas.drawRect(
          Rect.fromLTRB(
              candlestick.centerX - (_candleWidth / 2),
              size.height - candlestick.candleHighY,
              candlestick.centerX + (_candleWidth / 2),
              size.height - candlestick.candleLowY),
          candlestick.paint);
    }
  }

  List<Candlestick> _generateCandlesticks(Size availableSpace) {
    final pixelsPerTimeWindow =
        availableSpace.width / (chartData.prices.length + 1);
    final windowHigh = chartData.maxPrice();
    final windowLow = chartData.minPrice();
    final pixelsPerUnit = availableSpace.height / (windowHigh - windowLow);

    List<Candlestick> candlesticks = [];
    for (int i = 0; i < chartData.prices.length; i++) {
      final price = chartData.prices[i];
      candlesticks.add(Candlestick(
        wickHighY: (price.high - windowLow) * pixelsPerUnit,
        wickLowY: (price.low - windowLow) * pixelsPerUnit,
        candleHighY: ((price.open > price.close ? price.open : price.close) -
                windowLow) *
            pixelsPerUnit,
        candleLowY: ((price.open > price.close ? price.close : price.open) -
                windowLow) *
            pixelsPerUnit,
        centerX: (i + 1) * pixelsPerTimeWindow,
        paint: price.isGain() ? _gainPaint : _lossPaint,
      ));
    }

    return candlesticks;
  }

  @override
  bool shouldRepaint(covariant CustomPainter oldDelegate) {
    return true;
  }
}

class Candlestick {
  final double wickHighY;
  final double wickLowY;
  final double candleHighY;
  final double candleLowY;
  final double centerX;
  final Paint paint;

  // The curly braces make these named parameters
  Candlestick(
      {@required this.wickHighY,
      @required this.wickLowY,
      @required this.candleHighY,
      @required this.candleLowY,
      @required this.centerX,
      @required this.paint});
}

class VolumePainter extends CustomPainter {
  final ChartData chartData;
  final Paint _gainPaint;
  final Paint _lossPaint;

  VolumePainter(this.chartData)
      : _gainPaint = Paint()..color = Colors.green.withOpacity(0.5),
        _lossPaint = Paint()..color = Colors.red.withOpacity(0.5);

  @override
  void paint(Canvas canvas, Size size) {
    if (chartData == null) {
      return;
    }

    List<VolumeBar> bars = _generateBars(size);
    for (VolumeBar bar in bars) {
      canvas.drawRect(
          Rect.fromLTWH(
              bar.centerX - (bar.width / 2),
              size.height - bar.height, // Think from top left
              bar.width,
              bar.height),
          bar.paint);
    }
  }

  List<VolumeBar> _generateBars(Size availableSpace) {
    final pixelsPerTimeWindow =
        availableSpace.width / (chartData.prices.length + 1);
    final pixelsPerUnit = availableSpace.height / chartData.maxVolume();

    List<VolumeBar> bars = [];
    for (int i = 0; i < chartData.prices.length; i++) {
      final price = chartData.prices[i];
      bars.add(VolumeBar(
        width: 3.0,
        height: price.volume * pixelsPerUnit,
        centerX: (i + 1) * pixelsPerTimeWindow,
        paint: price.isGain() ? _gainPaint : _lossPaint,
      ));
    }

    return bars;
  }

  @override
  bool shouldRepaint(covariant CustomPainter oldDelegate) {
    return true;
  }
}

class VolumeBar {
  final double width;
  final double height;
  final double centerX;
  final Paint paint;

  VolumeBar(
      {@required this.width,
      @required this.height,
      @required this.centerX,
      @required this.paint});
}

class ChartData {
  final List<PriceData> prices;

  ChartData(this.prices);

  int maxVolume() {
    int maxVolume = 0;
    for (PriceData price in prices) {
      if (price.volume > maxVolume) {
        maxVolume = price.volume;
      }
    }
    return maxVolume;
  }

  double maxPrice() {
    double maxPrice = 0;
    for (PriceData price in prices) {
      if (price.high > maxPrice) {
        maxPrice = price.high;
      }
    }
    return maxPrice;
  }

  double minPrice() {
    double minPrice = double.maxFinite;
    for (PriceData price in prices) {
      if (price.low < minPrice) {
        minPrice = price.low;
      }
    }
    return minPrice;
  }
}

class PriceData {
  final Int64 time;
  final double open;
  final double close;
  final double high;
  final double low;
  final int volume;

  PriceData(this.time, this.open, this.close, this.high, this.low, this.volume);

  bool isGain() {
    return close >= open;
  }
}
