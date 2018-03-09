

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.00.0603 */
/* at Fri May 13 15:26:17 2016
 */
/* Compiler settings for MFC.idl:
    Oicf, W1, Zp8, env=Win32 (32b run), target_arch=X86 8.00.0603 
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 475
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__


#ifndef __MFC_h_h__
#define __MFC_h_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IMFC_FWD_DEFINED__
#define __IMFC_FWD_DEFINED__
typedef interface IMFC IMFC;

#endif 	/* __IMFC_FWD_DEFINED__ */


#ifndef __CMFCDoc_FWD_DEFINED__
#define __CMFCDoc_FWD_DEFINED__

#ifdef __cplusplus
typedef class CMFCDoc CMFCDoc;
#else
typedef struct CMFCDoc CMFCDoc;
#endif /* __cplusplus */

#endif 	/* __CMFCDoc_FWD_DEFINED__ */


#ifdef __cplusplus
extern "C"{
#endif 



#ifndef __MFC_LIBRARY_DEFINED__
#define __MFC_LIBRARY_DEFINED__

/* library MFC */
/* [version][uuid] */ 


EXTERN_C const IID LIBID_MFC;

#ifndef __IMFC_DISPINTERFACE_DEFINED__
#define __IMFC_DISPINTERFACE_DEFINED__

/* dispinterface IMFC */
/* [uuid] */ 


EXTERN_C const IID DIID_IMFC;

#if defined(__cplusplus) && !defined(CINTERFACE)

    MIDL_INTERFACE("5FFAD769-A45F-4F13-974A-2A6B9CC8B2C6")
    IMFC : public IDispatch
    {
    };
    
#else 	/* C style interface */

    typedef struct IMFCVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IMFC * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IMFC * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IMFC * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IMFC * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IMFC * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IMFC * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IMFC * This,
            /* [annotation][in] */ 
            _In_  DISPID dispIdMember,
            /* [annotation][in] */ 
            _In_  REFIID riid,
            /* [annotation][in] */ 
            _In_  LCID lcid,
            /* [annotation][in] */ 
            _In_  WORD wFlags,
            /* [annotation][out][in] */ 
            _In_  DISPPARAMS *pDispParams,
            /* [annotation][out] */ 
            _Out_opt_  VARIANT *pVarResult,
            /* [annotation][out] */ 
            _Out_opt_  EXCEPINFO *pExcepInfo,
            /* [annotation][out] */ 
            _Out_opt_  UINT *puArgErr);
        
        END_INTERFACE
    } IMFCVtbl;

    interface IMFC
    {
        CONST_VTBL struct IMFCVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IMFC_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IMFC_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IMFC_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IMFC_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IMFC_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IMFC_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IMFC_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */


#endif 	/* __IMFC_DISPINTERFACE_DEFINED__ */


EXTERN_C const CLSID CLSID_CMFCDoc;

#ifdef __cplusplus

class DECLSPEC_UUID("F8C523A0-8A64-4016-8CAE-884A4F9E5AF6")
CMFCDoc;
#endif
#endif /* __MFC_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


