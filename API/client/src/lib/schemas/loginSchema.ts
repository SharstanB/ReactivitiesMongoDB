import {z} from 'zod'

const requiredEmail = (fieldName : string) => z.string({message:`${fieldName} is required`})
.email();

const  requiredPassword = (fieldName : string) => z.string({message:`${fieldName} is required`})
.min(8,{message: `${fieldName} is weak, please choose a stonger password`});

export const loginSchema = z.object({
    email : requiredEmail('email'),
    password :requiredPassword('password'),
})


export type LoginSchema = z.infer<typeof loginSchema>