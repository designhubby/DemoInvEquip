import logo from './logo.svg';
import './App.css';
import "react-datepicker/dist/react-datepicker.css";
import { Route, Navigate, Routes, Switch, useHistory, useNavigate } from 'react-router-dom';

import React, {useState, useEffect,useRef} from 'react';
import {  toast } from 'react-toastify';
import { ToastProvider, useToasts } from 'react-toast-notifications'
import 'react-toastify/dist/ReactToastify.css'
import {createBrowserHistory } from 'history';
import { PersonDeviceView } from './components/main/PersonDeviceView';
import { DeviceListViewTable } from './components/subcomponents/DeviceViewComponents/DeviceListViewTable';
import { DeviceView } from './components/main/DeviceListView';
import { General } from './components/main/General';
import DeviceForm from './components/subcomponents/DeviceForm';
import VendorForm from './components/subcomponents/VendorViewComponents/VendorForm';
import VendorView from './components/main/VendorView';
import DeviceTypeForm from './components/subcomponents/DeviceTypeViewComponents/DeviceTypeForm';
import DepartmentView from './components/main/DepartmentView';
import { RoleView } from './components/main/RoleView';
import VendorView2 from './components/main/VendorView2';
import { PersonView } from './components/main/PersonView';
import { DeviceTypeView2 } from './components/main/DeviceTypeView2';
import { ErrorTest } from './components/main/ErrorTest';
import HwModelView from './components/main/HwModelView';
import ContractView from './components/main/ContractView';
import { OwnedDevices } from './components/subcomponents/ContractViewComponents/OwnedDevices';
import { PersonDeviceDeviceHistory } from './components/subcomponents/PersonDeviceViewComponents/PersonDeviceDeviceHistory';
import { Signin } from './components/main/Signin';
import NavBar1 from './components/main/navbar1';
import {Register} from './components/main/Register';
import { UserInfo } from './components/main/UserInfoTest';
import { WebLogin, WebLogout, GetAuthenticationStatus } from './service/DataServiceAuth';
import { GetUserInfo } from './service/DataServiceUser';
import { Dashboard } from './components/main/Dashboard';


toast.configure(
  {
    autoClose:3,
  }
);

function App() {
  const navigate = useNavigate();
  const [signedIn, setSignedIn] = useState(false);
  const [user, setUser] = useState(null);

  const UserAuthorizationSetup = async ()=>{
    console.log(`run user auth setup`)
    const authorized = await GetAuthenticationStatus();
    if(!authorized){
      
        console.log('app.js redirect')
        setSignedIn(false);
        navigate("/main/Signin");
      }else{
        console.log(`appjs else`)
        const userInfo = await GetUserInfo();
        setUser(userInfo);
        setSignedIn(true);
      


    }
  }

  useEffect(()=>{

    UserAuthorizationSetup();    //check if user exists, get user info (if failed, direct to log in)
    
  },[])

  let SignedInState = {
    get state(){
      return signedIn;
    },
    /**
     * @param {boolean | ((prevState: boolean) => boolean)} state
     */
    set setState(state){
      if(state == true){
        setSignedIn(state);
      }
      if(state== false){
        setSignedIn(false);
      }
    }
  }



  const handleAuthenticationLogOut = async () =>{
    const authStatus = await GetAuthenticationStatus();
    //if Logged in 
    ////Sign Out
    if(authStatus){
      await WebLogout();
    }
    
    await Promise.resolve(setSignedIn(await GetAuthenticationStatus()));

  }

  let User={
    get current(){
      return user;
    },
    /**
     * @param {any} value
     */
    set setUser(value){
      setUser(value);
    }
  }

  return (
    <React.Fragment>
    <div className="Root-Background">
    <main className = "container bg-dark text-white ">
    <div className="Main-Header ">
      <ToastProvider autoDismissTimeout = '2000' autoDismiss = {true}>
    <NavBar1 SignedInState= {SignedInState} HandleAuthenticationLogOut = {handleAuthenticationLogOut} AuthenticationSetup = {UserAuthorizationSetup}/>
    <Routes>
      <Route 
        path ="/main/ErrorTestView" 
        element = {<ErrorTest/>}
      />
      <Route
        index element = {<Dashboard/>}/>
      <Route 
        path ="/main/PersonView" 
        element = {<PersonView/>}
      />
      <Route
        path = "/main/PersonDeviceView/:idPerson"
        element = {<PersonDeviceView/>}/>
      <Route
        path = "/main/HwModelListView"
        element = {<HwModelView/>}/>
      <Route
        path ="/main/DeviceListView/"
        element = {<DeviceView   />}/>
      <Route
        path = "/form/DeviceDataForm/:idDevice"
        element = {<DeviceForm/>}/>
      <Route
        path = "/main/VendorView"
        element = {<VendorView2/>}/>
      <Route
        path = "/main/VendorForm/:idVendor"
        element = {<VendorForm/>}/>

      <Route
        path ="/main/Dashboard/"
        element = {<Dashboard/>}/>
      <Route
        path ="/main/General/"
        element = {<General/>}/>
      <Route
        path ="/main/DeviceTypeView2/"
        element = {<DeviceTypeView2   />}/>
      <Route
        path = "/main/DeviceTypeForm"
        element = {<DeviceTypeForm/>}/>
      <Route 
        path = "/main/DepartmentView"
        element = {<DepartmentView/>}/>
      <Route 
        path = "/main/RoleView"
        element = {<RoleView/>}/>
      <Route 
        path = "/main/ContractView"
        element = {<ContractView/>}/>
      <Route 
        path = "/main/ContractView/OwnedDevices/:idContract"
        element = {<OwnedDevices/>}/>
      <Route 
        path = "/main/PersonDeviceViewDeviceHistory/:idDevice"
        element = {<PersonDeviceDeviceHistory/>}/>
      <Route 
        path = "/main/Signin"
        element = {<Signin SignedInState = {SignedInState} User ={User}/>}/>        
      <Route 
        path = "/main/Register"
        element = {<Register/>}/>          
      <Route 
        path = "/main/UserInfo"
        element = {<UserInfo SignedInState = {SignedInState}/>}/>           
    </Routes>
    </ToastProvider>
    </div>
    </main>
    </div>
    </React.Fragment>
  );
}

export default App;
