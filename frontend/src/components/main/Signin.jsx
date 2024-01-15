
import React, {useState, useRef, useEffect} from 'react';
import { useSearchParams, useNavigate } from "react-router-dom";
import { useParams } from "react-router";
import { Constants } from './../common/constants';
import { TextField,SubmitButton, GeneralButton, SelectField,DatePicker } from './../common/form';
import { WebLogin, WebLogout, GetAuthenticationStatus } from '../../service/DataServiceAuth';
import { useToasts } from 'react-toast-notifications';
import { GetUserInfo } from '../../service/DataServiceUser';
import Joi from 'joi';



export function Signin({SignedInState, User}){
    const navigate = useNavigate();
    const {addToast} =  useToasts();
    const formNames = {
        email : "email",
        password: "password"
    }

    const errorsBlank = {
        emailError : "",
        passwordError: "",
    }
    const [errors, setErrors] = useState(errorsBlank);

    const [submitbtndisabled, setSubmitbtndisabled] = useState(false);

    const signinSchema = Joi.object().keys({
        [formNames.email] : Joi.string().label('Email').email({minDomainSegments: 2, tlds : {allow: ['com', 'net', 'org', 'ca']}}).required(),
        [formNames.password]: Joi.string().label('Password').pattern(new RegExp('^[a-zA-Z0-9\\W]{3,30}$')).required(),
    }).options({allowUnknown: true});


    let localLoginState = SignedInState.state;
    //form data
    const [formData, setFormData] = useState({
        email: "",
        password : "",
    });


    const handleOnChange =(e)=>{
        console.log(`onChange`)
        const {name, value} = e.target
        const rule = signinSchema.extract(name.toString()) 
        const subSchema = Joi.object().keys({[name]: rule})
        const validationResults = subSchema.validate({[name] : value}, {abortEarly : false});
        //const validationResults = signinSchema.validate(formData, {abortEarly : false});
        console.log(validationResults);
        
        if(validationResults.error && validationResults.error.details[0]){
            const newErrorData = Object.assign({},{...errors}, {[name+'Error'] : validationResults.error.details[0].message})
            setErrors(newErrorData);
        }else{
            const newErrorData = Object.assign({}, {...errors}, {[name+'Error']: ""});
            setErrors(newErrorData);
        }
        const newFormData = Object.assign({},{...formData, [name]:value});

        setFormData(newFormData);
    }

    const handleOnSubmit= async (e)=>{
        e.preventDefault();
        const validationResult = signinSchema.validate(formData, {abortEarly: false});
        console.log(`ValidationResult`);
        const {error} = validationResult;
        let _errors = {};
        if(validationResult.error){
            validationResult.error.details.forEach(indiv=> {
                _errors =  Object.assign({},{...errors},{[indiv.context.key+'Error']: indiv.message })
            });
            console.log(`_errors`)
            console.log(_errors)
            setErrors(_errors);
            addToast("Please correct indicated fields")
        }else{
            try{
                setSubmitbtndisabled(true);
                setTimeout(() => setSubmitbtndisabled(false) , 5000);
                console.log(`submitting`)
                const result = await WebLogin(formData);
                addToast("Logged in!", {appearance :'success'});
                console.log(`result`)
                console.log(result)
                User.setUser = await GetUserInfo();
                console.log(`User.current`)
                console.log(User.current)
                
                await Promise.resolve(SignedInState.setState =true);
    
            }catch(err){
                console.log(`err.data`);
                console.log(err);
    
                if(err.data && err.data.exceptionDetails){
                    const msg = err.data.exceptionDetails[0].message;
                    if(msg.toLowerCase().indexOf('unique') != -1){
                        addToast('Email Already Exists', {appearance : 'error'});
                    }
                }else{
                    addToast(err.data.title, {appearance : 'error'});
                }
    
            }
        }
        

    }

    const signin = ()=>{
        return(
            <div className ="signin">
        
                <form className="form-signin" onSubmit = {handleOnSubmit}>
                    <img className="mb-4" src="https://getbootstrap.com/docs/4.0/assets/brand/bootstrap-solid.svg" alt="" width="72" height="72"/>
                    <h1 className="h3 mb-3 font-weight-normal">Please sign in</h1>
                    <label htmlFor="inputEmail" className="sr-only" >Email address</label>
                    <input type="email" id="inputEmail" name = {formNames.email} className="form-control" placeholder="Email address" required="" autoFocus="" onChange = {(e)=>handleOnChange(e)} data-statekey= {formNames.email} value = {formData.email}/>
                    <label htmlFor="inputEmail" className="sr-only form-error-label" >{errors.emailError}</label>
<br/>
                    <label htmlFor="inputPassword" className="sr-only">Password</label>
                    <input type="password" id="inputPassword" name = {formNames.password} className="form-control" placeholder="Password" required="" data-statekey= {formNames.password} value = {formData.password} onChange = {(e)=>handleOnChange(e)}/>
                    <label htmlFor="inputPassword" className="sr-only form-error-label">{errors.passwordError}</label>
                    <button className="btn btn-lg btn-primary btn-block" disabled = {submitbtndisabled} type="submit">Sign in</button>
                </form>
                
            </div>
            
        )
    }

    const signedin = () =>
        (
        <>
        <h1>Welcome {User.current.firstName}</h1>
        <br/>
        <h1>Name : {User.current.userName} </h1>
        <form>
            
        </form>
        </>
        );

    return(

        localLoginState  ? signedin() : signin()
    )
    
}