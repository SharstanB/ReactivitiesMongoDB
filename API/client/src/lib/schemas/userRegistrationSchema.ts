import {z} from 'zod'

const requiredEmail = (fieldName : string) => z.string({message:`${fieldName} is required`})
.email();
const  requiredString = (fieldName : string) => z.string({message:`${fieldName} is required`}).min(1, 
        {message: `${fieldName} is required`});
const  requiredPassword = (fieldName : string) => z.string({message:`${fieldName} is required`})
.min(8,{message: `${fieldName} is weak, please choose a stonger password`});
const dateRules = (fieldName : string) => z.coerce.date({message: `${fieldName} is required`}).refine((val) => {
    const inputDate = new Date(val);
    const now = new Date();
    return inputDate > now;
  }, {
    message: "Date must be in the future",
  });
export const loginSchema = z.object({
    name: requiredString('name'),
    email : requiredEmail('email'),
    password :requiredPassword('password'),
    DOB : dateRules('DOB'),
})


export type LoginSchema = z.infer<typeof loginSchema>