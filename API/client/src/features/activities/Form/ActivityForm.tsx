import {
  Box,
  Button,
  Paper,
  Typography,
} from "@mui/material";
import { useEffect } from "react";
import { useActivities } from "../../../lib/hooks/useActivities";
import { useCategories } from "../../../lib/hooks/useCategories";
import { Link, useNavigate, useParams } from "react-router";
import { useForm } from "react-hook-form";
import {
  activitySchema,
  ActivitySchema,
} from "../../../lib/schemas/activitySchema";
import { zodResolver } from "@hookform/resolvers/zod";
import TextInput from "../../../app/share/components/TextInput";
import SelectInput from "../../../app/share/components/SelectInput";
import DataInput from "../../../app/share/components/DataInput";
import LocationInput from "../../../app/share/components/LocationInput";
import { Activity } from "../../../lib/types";
export default function ActivityForm() {

  const {reset,handleSubmit, control } = useForm<ActivitySchema>({
    mode: "onTouched",
    resolver: zodResolver(activitySchema),
  });

  const navigate = useNavigate();
  const { id } = useParams();
  const { updateActivity, createActivity, activityDetail, isLoadingActivity } =
    useActivities(id);
  // const { citieslist } = useCities();
  const { categorieslist } = useCategories();

  // You receive new activity details from an API and want to auto-fill the form:
  useEffect(() => {
    if (activityDetail) reset({
      ...activityDetail,
      location: {
        city: activityDetail.city,
        venue: activityDetail.venue,
        latitude: activityDetail.latitude,
        longitude: activityDetail.longitude
      }
    });
  }, [activityDetail, reset]);
  const onSubmit = (data: ActivitySchema) => {
       const{location , ...rest} = data;
       const flattenedData = {...rest, ...location};
       try {
        console.log(activityDetail);
        if(activityDetail){
          updateActivity.mutate({...activityDetail, ...flattenedData},{
             onSuccess: () => navigate(`/activities/${activityDetail.id}`)
          })
        }else{
          createActivity.mutate(flattenedData as Activity, {
            onSuccess: (data) => { 
              console.log(data);
              navigate(`/activities/${data}`) }
          });
        }
       } catch (error) {
        console.log(error);
       }
      
  };

  if (isLoadingActivity) return <Typography> isLoading...</Typography>;
  return (
    <Paper sx={{ borderRadius: 3, padding: 3 }}>
      <Typography gutterBottom variant="h5" color="warning">
        {activityDetail ? "Edit Activity" : "Create Activity"}
      </Typography>

      <Box
        component="form"
        onSubmit={handleSubmit(onSubmit)}
        sx={{ display: "flex", flexDirection: "column", gap: 3 }}
      >
        <TextInput 
          label='Title'
          control= {control}
          name="title"></TextInput> 
          <Box sx={{display: 'flex', gap: 4}}>
            <SelectInput label='Category' 
              items={categorieslist} 
              name='categoryId' 
              control={control}  >
            </SelectInput>
            <DataInput
              control={control}
              name='date'
              label='Date'
            />
          </Box>
        
        <TextInput
          label='Description'
          multiline
          rows={3}
          control ={control}
          name='description'
        ></TextInput>
      
        <LocationInput 
        control={control} 
        lable='Enter the location' 
        name='location'>
        </LocationInput>
        
        <Box sx={{ display: "flex", justifyContent: "end", gap: 3, mt: 4 }}>
          <Button component={Link} to={"/activities"} color="inherit">
            Cancle
          </Button>
          <Button
            color="success"
            variant="contained"
            type="submit"
            disabled={updateActivity.isPending || createActivity.isPending}>
            Submit
          </Button>
        </Box>
      </Box>
    </Paper>
  );
}
