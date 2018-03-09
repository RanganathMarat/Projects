#include <QApplication>
#include <QQmlApplicationEngine>


enum class A: int {
    bla=1, blu=2
};

int main(int argc, char *argv[])
{
    QApplication app(argc, argv);

    QQmlApplicationEngine engine;
    engine.load(QUrl(QStringLiteral("qrc:/main.qml")));

    return app.exec();
}
