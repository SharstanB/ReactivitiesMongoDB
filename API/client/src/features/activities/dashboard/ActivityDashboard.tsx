import {Grid} from "@mui/material";
import ActivityList from "./ActivityList";
import ActivityFilters from "./filters/ActivityFilters";
import ActivitySelectDate from "./filters/ActivitySelectDate";
// import SelectDate from "./filters/ActivitySelectDate";


export default function ActivityDashboard() {
    // export default function ActivityDashboard(props: Props) {     
    //  in case of using this way ,then down we need to access the activites as {props.activities..}

  return (
    <Grid container spacing={3}>
    <Grid size={8}>
       <ActivityList></ActivityList>
    </Grid>
    <Grid size={4}>
      <ActivityFilters></ActivityFilters>
      <ActivitySelectDate></ActivitySelectDate>
    </Grid>
    </Grid>
 
    )
}
