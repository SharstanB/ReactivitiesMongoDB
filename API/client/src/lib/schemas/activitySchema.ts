import {z} from 'zod'

const requiredString = (fieldName : string) => z.string({message:`${fieldName} is required`}).min(1, 
    {message: `${fieldName} is required`});
const dateRules = (fieldName : string) => z.coerce.date({message: `${fieldName} is required`}).refine((val) => {
    const inputDate = new Date(val);
    const now = new Date();
    return inputDate > now;
  }, {
    message: "Date must be in the future",
  });
export const activitySchema = z.object({
    title : requiredString('title'),
    description : requiredString('description'),
    date : dateRules('date'),
    categoryId : requiredString('category'),
    location : z.object({
        latitude : z.coerce.number(),
        longitude : z.coerce.number(),
        venue : requiredString('Venue'),
        city: z.string().optional(),
    })
    // venue :requiredString('venue'),
    //  cityId : requiredString('city'),
})


export type ActivitySchema = z.infer<typeof activitySchema>