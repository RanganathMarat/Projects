#include "examcardcontrol.h"
#include "ui_examcardcontrol.h"
#include "rowcontrol.h"
#include <QElapsedTimer>

QListWidget *list1;
ExamCardControl::ExamCardControl(QWidget *parent) :
    QFrame(parent),
    ui(new Ui::ExamCardControl)
{
    ui->setupUi(this);
    this->setMaximumHeight(200);
   this->setMaximumWidth(1000);

}


ExamCardControl::~ExamCardControl()
{
    delete ui;
}

void ExamCardControl::on_pushButton_clicked()
{   
     QElapsedTimer myTimer;
      myTimer.start();
    for(int i=0;i<1000;i++)
    {

    QListWidgetItem *item=new QListWidgetItem(ui->listWidget);
    RowControl *row=new RowControl(ui->listWidget);
    row->SetInteractor(i);
    item->setSizeHint(row->maximumSize());
    ui->listWidget->setItemWidget(item,row);

    }
     ui->timeLabel->setText( QString::number(myTimer.elapsed())+ " ms");
}
