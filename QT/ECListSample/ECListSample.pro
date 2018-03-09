#-------------------------------------------------
#
# Project created by QtCreator 2015-02-16T16:08:31
#
#-------------------------------------------------

QT       += core gui

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

TARGET = ECListSample
TEMPLATE = app
t

SOURCES += main.cpp\
        mainwindow.cpp \
    rowcontrol.cpp \
    examcardcontrol.cpp

HEADERS  += mainwindow.h \
    rowcontrol.h \
    examcardcontrol.h

FORMS    += mainwindow.ui \
    rowcontrol.ui \
    examcardcontrol.ui

RESOURCES += \
    resourcesFiles.qrc
