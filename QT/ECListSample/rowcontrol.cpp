#include "rowcontrol.h"
#include "ui_rowcontrol.h"

#include <QtGlobal>
RowControl::RowControl(QWidget *parent) :
    QFrame(parent),
    ui(new Ui::RowControl)
{
    ui->setupUi(this);
    this->setMaximumHeight(60);
    this->setMaximumWidth(180);
}
void RowControl::SetInteractor(int iterator)
{

    //QString s = QString::number(iterator);
    ui->scanNr->setText( "1, " + QString::number(iterator));

    //ui->scanNr->setTextFormat(Qt::QString::number("1," + iterator));
}

RowControl::~RowControl()
{
    delete ui;
}
