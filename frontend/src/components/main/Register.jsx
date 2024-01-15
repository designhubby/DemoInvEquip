import React, {useState, useRef,useEffect} from 'react';
import { Route, Navigate, Routes, Switch, useHistory, useNavigate } from 'react-router-dom';
import { WebRegister } from '../../service/DataServiceAuth';
import { useToasts } from 'react-toast-notifications';
import Joi from 'joi';

export function Register(){
    
    const navigate = useNavigate();
    const {addToast} =  useToasts();

    const [redirect, setRedirect] = useState(false);

    //Constants
    
    const formNames = {
        email : "email",
        password: "password",
        userName : "userName",
        inviteCode : "inviteCode",
        firstName: "firstName",
        lastName:"lastName",
    }

    //form data

    const [formData, setFormData] = useState({
        id: 0,
        email: "",
        userName : "",
        password : "",
        firstName : "",
        lastName: "",
        inviteCode : "",
    });

    //joi


    const errorsBlank = {
        emailError : "",
        passwordError: "",
        userNameError: "",
        inviteCodeError: "",
    }
    const [errors, setErrors] = useState(errorsBlank);

    const registrationSchema = Joi.object().keys({
        [formNames.email] : Joi.string().label('Email').email({minDomainSegments: 2, tlds : {allow: ['com', 'net', 'org', 'ca']}}).required(),
        [formNames.password] : Joi.string().label('Password').pattern(new RegExp('^[a-zA-Z0-9\\W]{3,30}$')).required(),
        [formNames.userName] : Joi.string().label("Display Name").min(3).max(30).required(),
        [formNames.inviteCode] : Joi.string().label("Invite Code").min(3).max(30).required(),
        [formNames.firstName] : Joi.string().label("First Name"),
        [formNames.lastName] : Joi.string().label("Last Name"),

    }).options({allowUnknown: true});


    const handleOnChange = (e) => {
        const {name, value} = e.target
        const rule = registrationSchema.extract(name.toString());
        const subSchema = Joi.object().keys({
            [name] : rule,
        })
        const validationResults = subSchema.validate({[name] : value}, {abortEarly : false});
        console.log(validationResults);
        if(validationResults.error && validationResults.error.details[0]){
            const errorObj = Object.assign({}, {...errors}, {[name+'Error']: validationResults.error.details[0].message} )
            setErrors(errorObj);
            console.log('errors')
            console.log(errors)
        }else{
            const errorObj = Object.assign({}, {...errors}, {[name+'Error']: ""} )
            setErrors(errorObj);
            console.log('errors')
            console.log(errors)
        }
        const newData = Object.assign({},{ ...formData, [name]: value})
        setFormData(newData);
        console.log("formData")
        console.log(formData)
        
    }
    const handleOnSubmit = async (e)=>{
        e.preventDefault();
        //validate form
        
        const validationResult = registrationSchema.validate(formData, {abortEarly: false});
        console.log('ValidationResult');
        console.log(validationResult);
        let _errors = {};
        if(validationResult.error){
            validationResult.error.details.forEach(indiv=>{
                _errors = Object.assign({}, {..._errors}, {[indiv.context.key+'Error']: indiv.message});
            })
            setErrors(_errors);
            console.log(`Submit _errors`)
            console.log(_errors)
            addToast("Please correct indicated fields")
        }else{
            console.log('submitting')
            try{
                const result = await WebRegister(formData);
                console.log(result);
                addToast("Saved!", {appearance :'success'});
                setRedirect(true);
    
            }catch(err){
                console.log(`err.data`);
                addToast(err.data.title, {appearance : 'error'});
    
            }
        }

    }

    if(redirect){
        navigate("/main/Signin")
    }


    return(
        <div className ="signin">

        <form className="form-signin" onSubmit={handleOnSubmit}>
            <img className="mb-4" src="https://getbootstrap.com/docs/4.0/assets/brand/bootstrap-solid.svg" alt="" width="72" height="72"/>
            <h1 className="h3 mb-3 font-weight-normal">Please fill in your account info</h1>

            <label htmlFor="inputInvite" className="sr-only">Invite Code</label>
            <input type="password" name = {formNames.inviteCode} id="inputInvite" className="form-control" placeholder="Required" required="" autofocus="" data-statekey= {formNames.inviteCode}  value = {formData.inviteCode} onChange = {(e)=>handleOnChange(e)}/>
            <label htmlFor="inputInvite" className="sr-only form-error-label"  >{errors.inviteCodeError}</label>
            <br/>
            
            <label htmlFor="inputEmail" className="sr-only">Email address</label>
            <input type="email" name = {formNames.email} id="inputEmail" className="form-control" placeholder="Required" required="" autofocus="" data-statekey= {formNames.email}  value = {formData.email} onChange = {(e)=>handleOnChange(e)}/>
            <label htmlFor="inputEmail" className="sr-only form-error-label"  >{errors.emailError}</label>
            <br/>
            <label htmlFor="inputPassword" className="sr-only">Password</label>
            <input type="password" name = {formNames.password} id="inputPassword" className="form-control" placeholder="Required" required=""  data-statekey= {formNames.password} value = {formData.password} onChange = {(e)=>handleOnChange(e)}/>
            <label htmlFor="inputPassword" className="sr-only form-error-label">{errors.passwordError}</label>
            <br/>
            <label htmlFor="userName" className="sr-only">Display Name</label>
            <input type="text" name = {formNames.userName} id="userName" className="form-control" placeholder="Required" required="" autofocus="" data-statekey= {formNames.userName} value = {formData.userName} onChange = {(e)=>handleOnChange(e)}/>
            <label htmlFor="userName" className="sr-only form-error-label">{errors.userNameError}</label>    
            <br/>
            <label htmlFor="inputFname" className="sr-only">First Name</label>
            <input type="text" name = "firstName" id="inputFname" className="form-control" placeholder="First Name (optional)" required="" autofocus="" data-statekey= {"firstName"} value = {formData.firstName} onChange = {(e)=>handleOnChange(e)}/>
            
            <label htmlFor="inputLname" className="sr-only">Last Name</label>
            <input type="text" name = "lastName"  id="inputLname" className="form-control" placeholder="Last Name (optional)" required="" autofocus="" data-statekey= {"lastName"} value = {formData.lastName} onChange = {(e)=>handleOnChange(e)}/>
            
            <br/>
            <button className="btn btn-lg btn-primary btn-block" type="submit">Create Account</button>
        </form>
        
    </div>        
    )
}