#include "samplecontrol.h"
#include "ui_samplecontrol.h"

SampleControl::SampleControl(QWidget *parent) :
    QFrame(parent),
    ui(new Ui::SampleControl)
{
    ui->setupUi(this);
}

SampleControl::~SampleControl()
{
    delete ui;
}
