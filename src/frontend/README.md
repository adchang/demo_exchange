# Frontend

A Flutter frontend using gRPC

## Android
### Local debug / [Wireless ADB](https://developer.android.com/studio/command-line/adb)
    adb connect CLIENT_IP

After which, the device should show up in:

    adb devices
OR

    flutter devices

In vscode or Android Studio, select main.dart and click Run

### Build
1. Remove -SNAPSHOT in pubspec.yaml, commit & push
2. Run

        demo-exchange-frontend-build

NOTE: apk is located in build/app/outputs/apk/production/release

3. Increment version num and add -SNAPSHOT in pubspec.yaml, commit & push

NOTE: Flutter parses the version value using +, where to the left of + is equal to versionName and the right is versionCode. Version code should be an incrementing number to denote sequence of versions, with a higher number indicating more recent version. You want to increment both the versionName and versionCode, ie. if you just released 0.0.2+2, the next version would be 0.0.3-SNAPSHOT+3, and when you release, it would be 0.0.3+3.

#### Versioned

1. If you want to have a different name for the app, edit android/app/build.gradle. In the versioned section of flavors, update the app name as appropriate
2. Run

        demo-exchange-frontend-build-versioned

NOTE: apk is located in build/app/outputs/apk/flutter-apk

## iOS
TODO

## Web

### Dev
    flutter run -d web --web-port 8489 --web-hostname 0.0.0.0

### Prod
    flutter build web
    cd build/web
    dhttpd --port=8489