import {  Grid, Typography } from '@mui/material'
import { useParams } from 'react-router';
import { useActivities } from '../../../lib/hooks/useActivities';
import ActivityDetailsHeader from './ActivityDetailsHeader';
import ActivityDetailsSidebar from './ActivityDetailsSidebar';
import ActivityDetailsInfo from './ActivityDetailsInfo';
import ActivityDetailsChat from './ActivityDetailsChat';


export default function ActivityDetailsPage() {

  const {id} = useParams();
  const {activityDetail, isLoadingActivity} = useActivities(id);

  if(!activityDetail) return <Typography> Activity Doesn't Found</Typography>
  if(isLoadingActivity) return <Typography> Loading ... </Typography>
  return (
    <Grid container spacing={3}>
      <Grid  size={8}>
         <ActivityDetailsHeader activity={activityDetail}></ActivityDetailsHeader>
         <ActivityDetailsInfo activity={activityDetail}></ActivityDetailsInfo>
         <ActivityDetailsChat></ActivityDetailsChat>
      </Grid>
      <Grid size={4}>
         <ActivityDetailsSidebar></ActivityDetailsSidebar>
      </Grid>
    </Grid>
  )
}
