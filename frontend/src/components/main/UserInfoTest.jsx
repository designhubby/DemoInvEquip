import React, {useState, useRef, useEffect} from 'react';
import { useSearchParams, useNavigate } from "react-router-dom";
import { useParams } from "react-router";
import { Constants } from './../common/constants';
import { TextField,SubmitButton, GeneralButton, SelectField,DatePicker } from './../common/form';
import { GetAuthenticationStatus, WebLogin } from '../../service/DataServiceAuth';
import { useToasts } from 'react-toast-notifications';
import { ChangeUserPassword, GetUserInfo,EditUserInfo } from '../../service/DataServiceUser';
import Cookies from 'universal-cookie';

export function  UserInfo({SignedInState}){
    const {addToast} = useToasts();
    const [userData, setUserData] = useState({
        
        id : "",
        userName: "",
        firstName : "",
        lastName : "",
        email: "",

    });
    const [sendUserData, setSendUserData] = useState({
        id : "",
        userName: "",
        firstName : "",
        lastName : "",
        email: "",        
    })

    const PasswordShow = {
        PasswordFieldOriginal : 'PasswordFieldOriginal',
        PasswordField1 : 'PasswordField1',
        PasswordField2: 'PasswordField2',
        None : 'None',
    }
    const btnNames = {
        btnShowPwd1 : 'btnShowPwd1',
        btnShowPwd2: 'btnShowPwd2',
    }

    const [passwordOriginal, setPasswordOriginal] = useState('');
    const [passwordNew1, setPasswordNew1] = useState('');
    const [passwordNew2, setPasswordNew2] = useState('');
    const [passwordShowState, setPasswordShowState] = useState(PasswordShow.None);

    const getData = async ()=>{
        const loginStatus = await GetAuthenticationStatus();
        await Promise.resolve(console.log(loginStatus));
        if(loginStatus){
            console.log(`await GetUserInfo();`)
            try{
                const data = await GetUserInfo();
                console.log(data);
                await Promise.resolve(setUserData(data));
            }catch(ex){
                console.log("User Info")
                addToast(ex.data.Title ?? "undefined error", {appearance : 'error', placement : 'top-center'})
            }
        }
        if(!loginStatus){
            SignedInState.setState = false;
            addToast("User Not Logged in", {appearance : 'error', placement : 'top-center'})
        }

    }

    //get userinfo
    useEffect(()=>{

        getData();

    },[])

    const handleCookie= ()=>{

    };

    const handleUserDataChange = (e)=>{
        console.log("Running handleUserDataChange")
        const key = e.target.name;
        const value = e.target.value;
        const _userData = Object.assign({}, {...userData}, {[key]: value});
        console.log(_userData);
        setUserData(_userData);
    }

    const handleOnSubmit = async (e)=>{
        console.log('default prevented')
        e.preventDefault();
        const passwordDto = {
            currentPwd : passwordOriginal,
            newPwd : passwordNew1,
        }
        const passwordEqual = passwordNew1 == passwordNew2;
        if(!passwordEqual){
            addToast("Passwords not identical",  {appearance : 'error', placement : 'top-center'});
        }else{
            try{
                await ChangeUserPassword(passwordDto)
                addToast("Passwords Change Succeeded",  {appearance : 'success', placement : 'top-center'});
            }catch(err){
                console.log(err);
                addToast("Passwords Change Failed",  {appearance : 'error', placement : 'top-center'});
            }

        }
    }

    const handleUserInfoChangeSubmit = async ()=>{
        console.log("Running handleUserInfoChangeSubmit")
        try{
            const _senduserData = Object.assign({},{...userData});
            console.log(`_senduserData`);
            console.log(_senduserData);
            setSendUserData(_senduserData);
            await EditUserInfo(_senduserData);
            addToast("User Info Changed", {appearance: "success"});
        }catch(ex){
            console.log(ex);
            addToast("Edit Failed", {appearance : "error"});
        }

    }

    const handleShowPassword = (e) =>{
        if(e.target.name == btnNames.btnShowPwd1){
            setPasswordShowState(PasswordShow.PasswordField1)

        }
        if(e.target.name == btnNames.btnShowPwd2){
            setPasswordShowState(PasswordShow.PasswordField2)
        }

    }



    return(
        <>
        <h1>User Control Panel</h1>
        <form onSubmit = {(e)=>handleUserInfoChangeSubmit(e)}>
            <div className = "border p-3">
        <h1> User Info </h1>
                <TextField label = 'User ID' name = "id" disabled = 'true' onChange = {handleUserDataChange} value = {userData.id}/>
                <TextField label = 'User Name' name = "username" onChange = {handleUserDataChange} value = {userData.userName}/>
                <TextField label = 'First Name' name = "firstName" onChange = {handleUserDataChange} value = {userData.firstName}/>
                <TextField label = 'Last Name' name = "lastName" onChange = {handleUserDataChange} value = {userData.lastName}/>
                <TextField label = 'Email' name = "email" onChange = {handleUserDataChange} value = {userData.email}/>
                <GeneralButton label = 'Save User Info' handleOnClick={handleUserInfoChangeSubmit}/>
                
            </div>



        </form>

        <div className = 'border p-3'>
            <form onSubmit={(e)=>{handleOnSubmit(e)}}>
                <h1>Change Password</h1>
                <div className ='row mb-3'>
                    <label htmlFor='inputPasswordOriginal' className = 'col-sm-2 col-form-label'>Original Password</label>
                
                    <div className = 'me-0 pe-0 col-sm-5'> 
                        <input id = 'inputPasswordOriginal' className = 'form-control' name = 'inputPasswordOriginal' type = {passwordShowState == PasswordShow.PasswordFieldOriginal ? 'text' : 'password'}  value = {passwordOriginal} onChange ={(e)=>setPasswordOriginal(e.target.value)}/>
                    </div>
                </div>
                <div className ='row mb-3'>
                    <label htmlFor='inputPasswordNew1'  className = 'col-sm-2 col-form-label'>New Password</label>
                    <div className = 'me-0 pe-0 col-sm-5'>                     
                        <input id = 'inputPasswordNew1' className = 'form-control' name = 'inputPasswordNew1' type = {passwordShowState == PasswordShow.PasswordField1 ? 'text' : 'password'} value = {passwordNew1} onChange = {(e)=>setPasswordNew1(e.target.value)} />
                    </div>
                    <div className = 'm-0 p-0 col-sm-1'>    
                        <button type = 'button' name = {btnNames.btnShowPwd1} className = 'btn btn-primary' onMouseDown = {()=>setPasswordShowState(PasswordShow.PasswordField1)} onMouseUp={()=>setPasswordShowState(PasswordShow.None)}>{passwordShowState == PasswordShow.PasswordField1 ? <i className="bi bi-eye-fill"></i> : <i className="bi bi-eye-slash-fill"></i>}</button> 
                    </div>
                </div>

                <div className ='row mb-3'>
                    <label htmlFor='inputPasswordNew2' className = 'col-sm-2 col-form-label' >Confirm Password</label>
                    <div className = 'me-0 pe-0 col-sm-5'>  
                    <input id = 'inputPasswordNew2'  className = 'form-control' name = 'inputPasswordNew2' type = {passwordShowState == PasswordShow.PasswordField2 ? 'text' : 'password'} value = {passwordNew2} onChange = {(e)=>setPasswordNew2(e.target.value)} />
                    </div>
                    <div className = 'm-0 p-0 col-sm-1'> 
                        <button type = 'button' name = {btnNames.btnShowPwd2}  className = 'btn btn-primary' onMouseDown = {()=>setPasswordShowState(PasswordShow.PasswordField2)} onMouseUp={()=>setPasswordShowState(PasswordShow.None)}>{passwordShowState == PasswordShow.PasswordField2 ? <i className="bi bi-eye-fill"></i> : <i className="bi bi-eye-slash-fill"></i>}</button>
                    </div>
                </div> 
                <div className ='row mb-3'>
                    <div className = 'col-sm-3'>

                        <button type = 'submit' className='btn btn-primary'>Change Password</button>
                        </div>
                </div>
            </form>
        </div>
        </>
    )

}