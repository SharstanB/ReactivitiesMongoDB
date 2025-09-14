import {
  Avatar,
  Box,
  Button,
  Card,
  CardContent,
  Typography,
} from "@mui/material";
import { Link } from "react-router";
import { AccessTime, LocationOn } from "@mui/icons-material";
import { FormatDate } from "../../../lib/util/util";
import { Activity } from "../../../lib/types";
type Props = {
  activity: Activity;
};
export default function ActivityCard({activity}: Props) {

  return (
    <Card sx={{ borderRadius: 3 }} key={activity.id}>
      <CardContent>
        <Box style={{ display: 'flex', alignItems: 'center' }}>
          <Avatar sx={{ width: 100, height: 100 , margin:'0px 15px'}} alt="activity category" 
          src={`/images/categoryimages/${activity.categoryName}.jpg`} />
          <div style={{ marginLeft: '10px' }}>
            <Typography variant="h6">{activity.title}</Typography>
            <Typography>Hosted by&nbsp;&nbsp;<Link to={'/portofolie/'} ><a href="#" >Bob</a></Link></Typography>
          </div>
        </Box>
        <Box sx={{display:'flex' , marginTop:'10px'}}>
        <div style={{ display: 'flex', alignItems: 'center' }}>
          <AccessTime  style={{ marginRight: '10px' }}  />
          <Typography variant="body2">{FormatDate(activity.date)}</Typography>
        </div>
        <div style={{ display: 'flex', alignItems: 'center', marginLeft: '15px' }}>
          <LocationOn style={{ marginRight: '5px' }} />
          <Typography variant="body2">{activity.city}</Typography>
        </div>
        </Box>
       
        <div style={{ marginTop: '20px', padding: '10px', backgroundColor: '#f0f0f0' }}>
          <Typography variant="body2">Attendees go here</Typography>
        </div>
        </CardContent>
        <CardContent sx={{display:'flex', justifyContent:'space-between'}}>
        <Typography variant="body2" >{activity.description}</Typography>
        <Button component={Link} to={`/activities/${activity.id}`} variant="contained"
         color="primary" style={{ marginTop: '20px', borderRadius: 3 }}>
          View
        </Button>
      </CardContent>
    </Card>
  );
}
