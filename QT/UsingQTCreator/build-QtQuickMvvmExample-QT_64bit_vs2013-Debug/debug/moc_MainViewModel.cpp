/****************************************************************************
** Meta object code from reading C++ file 'MainViewModel.h'
**
** Created by: The Qt Meta Object Compiler version 67 (Qt 5.4.0)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include "../../Qtquickmvvmexample/ViewModel/MainViewModel.h"
#include <QtCore/qbytearray.h>
#include <QtCore/qmetatype.h>
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'MainViewModel.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 67
#error "This file was generated using the moc from 5.4.0. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
struct qt_meta_stringdata_MainViewModel_t {
    QByteArrayData data[11];
    char stringdata[148];
};
#define QT_MOC_LITERAL(idx, ofs, len) \
    Q_STATIC_BYTE_ARRAY_DATA_HEADER_INITIALIZER_WITH_OFFSET(len, \
    qptrdiff(offsetof(qt_meta_stringdata_MainViewModel_t, stringdata) + ofs \
        - idx * sizeof(QByteArrayData)) \
    )
static const qt_meta_stringdata_MainViewModel_t qt_meta_stringdata_MainViewModel = {
    {
QT_MOC_LITERAL(0, 0, 13), // "MainViewModel"
QT_MOC_LITERAL(1, 14, 18), // "SourceChangedEvent"
QT_MOC_LITERAL(2, 33, 0), // ""
QT_MOC_LITERAL(3, 34, 8), // "QString&"
QT_MOC_LITERAL(4, 43, 3), // "arg"
QT_MOC_LITERAL(5, 47, 23), // "DestinationChangedEvent"
QT_MOC_LITERAL(6, 71, 14), // "SetSourceValue"
QT_MOC_LITERAL(7, 86, 19), // "SetDestinationValue"
QT_MOC_LITERAL(8, 106, 12), // "clearCommand"
QT_MOC_LITERAL(9, 119, 11), // "sourceValue"
QT_MOC_LITERAL(10, 131, 16) // "destinationValue"

    },
    "MainViewModel\0SourceChangedEvent\0\0"
    "QString&\0arg\0DestinationChangedEvent\0"
    "SetSourceValue\0SetDestinationValue\0"
    "clearCommand\0sourceValue\0destinationValue"
};
#undef QT_MOC_LITERAL

static const uint qt_meta_data_MainViewModel[] = {

 // content:
       7,       // revision
       0,       // classname
       0,    0, // classinfo
       5,   14, // methods
       2,   52, // properties
       0,    0, // enums/sets
       0,    0, // constructors
       0,       // flags
       2,       // signalCount

 // signals: name, argc, parameters, tag, flags
       1,    1,   39,    2, 0x06 /* Public */,
       5,    1,   42,    2, 0x06 /* Public */,

 // slots: name, argc, parameters, tag, flags
       6,    1,   45,    2, 0x0a /* Public */,
       7,    1,   48,    2, 0x0a /* Public */,

 // methods: name, argc, parameters, tag, flags
       8,    0,   51,    2, 0x02 /* Public */,

 // signals: parameters
    QMetaType::Void, 0x80000000 | 3,    4,
    QMetaType::Void, 0x80000000 | 3,    4,

 // slots: parameters
    QMetaType::Void, 0x80000000 | 3,    4,
    QMetaType::Void, 0x80000000 | 3,    4,

 // methods: parameters
    QMetaType::Void,

 // properties: name, type, flags
       9, QMetaType::QString, 0x00495003,
      10, QMetaType::QString, 0x00495003,

 // properties: notify_signal_id
       0,
       1,

       0        // eod
};

void MainViewModel::qt_static_metacall(QObject *_o, QMetaObject::Call _c, int _id, void **_a)
{
    if (_c == QMetaObject::InvokeMetaMethod) {
        MainViewModel *_t = static_cast<MainViewModel *>(_o);
        switch (_id) {
        case 0: _t->SourceChangedEvent((*reinterpret_cast< QString(*)>(_a[1]))); break;
        case 1: _t->DestinationChangedEvent((*reinterpret_cast< QString(*)>(_a[1]))); break;
        case 2: _t->SetSourceValue((*reinterpret_cast< QString(*)>(_a[1]))); break;
        case 3: _t->SetDestinationValue((*reinterpret_cast< QString(*)>(_a[1]))); break;
        case 4: _t->clearCommand(); break;
        default: ;
        }
    } else if (_c == QMetaObject::IndexOfMethod) {
        int *result = reinterpret_cast<int *>(_a[0]);
        void **func = reinterpret_cast<void **>(_a[1]);
        {
            typedef void (MainViewModel::*_t)(QString & );
            if (*reinterpret_cast<_t *>(func) == static_cast<_t>(&MainViewModel::SourceChangedEvent)) {
                *result = 0;
            }
        }
        {
            typedef void (MainViewModel::*_t)(QString & );
            if (*reinterpret_cast<_t *>(func) == static_cast<_t>(&MainViewModel::DestinationChangedEvent)) {
                *result = 1;
            }
        }
    }
}

const QMetaObject MainViewModel::staticMetaObject = {
    { &QObject::staticMetaObject, qt_meta_stringdata_MainViewModel.data,
      qt_meta_data_MainViewModel,  qt_static_metacall, Q_NULLPTR, Q_NULLPTR}
};


const QMetaObject *MainViewModel::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->dynamicMetaObject() : &staticMetaObject;
}

void *MainViewModel::qt_metacast(const char *_clname)
{
    if (!_clname) return Q_NULLPTR;
    if (!strcmp(_clname, qt_meta_stringdata_MainViewModel.stringdata))
        return static_cast<void*>(const_cast< MainViewModel*>(this));
    return QObject::qt_metacast(_clname);
}

int MainViewModel::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = QObject::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        if (_id < 5)
            qt_static_metacall(this, _c, _id, _a);
        _id -= 5;
    } else if (_c == QMetaObject::RegisterMethodArgumentMetaType) {
        if (_id < 5)
            *reinterpret_cast<int*>(_a[0]) = -1;
        _id -= 5;
    }
#ifndef QT_NO_PROPERTIES
      else if (_c == QMetaObject::ReadProperty) {
        void *_v = _a[0];
        switch (_id) {
        case 0: *reinterpret_cast< QString*>(_v) = GetSourceValue(); break;
        case 1: *reinterpret_cast< QString*>(_v) = GetDestinationValue(); break;
        default: break;
        }
        _id -= 2;
    } else if (_c == QMetaObject::WriteProperty) {
        void *_v = _a[0];
        switch (_id) {
        case 0: SetSourceValue(*reinterpret_cast< QString*>(_v)); break;
        case 1: SetDestinationValue(*reinterpret_cast< QString*>(_v)); break;
        default: break;
        }
        _id -= 2;
    } else if (_c == QMetaObject::ResetProperty) {
        _id -= 2;
    } else if (_c == QMetaObject::QueryPropertyDesignable) {
        _id -= 2;
    } else if (_c == QMetaObject::QueryPropertyScriptable) {
        _id -= 2;
    } else if (_c == QMetaObject::QueryPropertyStored) {
        _id -= 2;
    } else if (_c == QMetaObject::QueryPropertyEditable) {
        _id -= 2;
    } else if (_c == QMetaObject::QueryPropertyUser) {
        _id -= 2;
    } else if (_c == QMetaObject::RegisterPropertyMetaType) {
        if (_id < 2)
            *reinterpret_cast<int*>(_a[0]) = -1;
        _id -= 2;
    }
#endif // QT_NO_PROPERTIES
    return _id;
}

// SIGNAL 0
void MainViewModel::SourceChangedEvent(QString & _t1)
{
    void *_a[] = { Q_NULLPTR, const_cast<void*>(reinterpret_cast<const void*>(&_t1)) };
    QMetaObject::activate(this, &staticMetaObject, 0, _a);
}

// SIGNAL 1
void MainViewModel::DestinationChangedEvent(QString & _t1)
{
    void *_a[] = { Q_NULLPTR, const_cast<void*>(reinterpret_cast<const void*>(&_t1)) };
    QMetaObject::activate(this, &staticMetaObject, 1, _a);
}
QT_END_MOC_NAMESPACE
