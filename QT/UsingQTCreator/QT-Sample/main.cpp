#include "mainwindow.h"
#include <QApplication>
#include <Qstring>
#include <QstringList>
#include <QListView>
#include <QStringListModel>
int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    // Unindented for quoting purposes:
    /*QStringList numbers;
    numbers << "One" << "Two" << "Three" << "Four" << "Five";

    QAbstractItemModel *model = new QStringListModel(numbers);
    QListView *view = new QListView;
    view->setModel(model);
    view->show();*/
    MainWindow w;
    w.showMaximized();

    return a.exec();
}
