#include "mainwindow.h"
#include <QApplication>
#include <QElapsedTimer>
#include <QMessageBox>


int main(int argc, char *argv[])
{
    QElapsedTimer myTimer;
     myTimer.start();
    QApplication a(argc, argv);
    MainWindow w;
    w.show();
    QMessageBox::information(NULL, "Application LoadTime", QString::number(myTimer.elapsed())+ " ms");
    return a.exec();
}
