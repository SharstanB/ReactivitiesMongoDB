import { Box, Button, Card, Typography } from '@mui/material';
import InfoIcon from '@mui/icons-material/Info';
import CalendarTodayIcon from '@mui/icons-material/CalendarToday';
import LocationOnIcon from '@mui/icons-material/LocationOn';
import { FormatDate } from '../../../lib/util/util';
import { Activity } from "../../../lib/types";
import { useState } from 'react';
import MapComponent from '../../../app/share/components/MapComponent';

type Props = {
 activity : Activity,
} 
export default function ActivityDetailsInfo({activity}: Props) {
      const [mapOpen, setMapOpen] = useState(false);
      return (
        <Card  sx={{ backgroundColor:'white',  padding: "16px", display: "flex", flexDirection: "column", gap: "16px" }}>
          <Box  sx={{ display: "flex", alignItems: "center", gap: "8px" }}>
            <InfoIcon color='info' />
            <Typography color='info' variant="body1">{activity.description}</Typography>
          </Box>
          
          <Box  sx={{ display: "flex", alignItems: "center", gap: "8px", margin:'10px 0', borderRadius: "4px" }}>
            <CalendarTodayIcon color='info' />
            <Typography color='info' variant="body1">{FormatDate(activity.date)}</Typography>
          </Box>
          
          <Box sx={{ display: "flex", alignItems: "center", gap: "8px" }}>
            <LocationOnIcon color='info' />
            <Typography color='info' variant="body1">{activity.venue}, {activity.city}</Typography>
            <Button sx={{textTransform:'none'}}onClick={()=> setMapOpen(!mapOpen)}  >
               {mapOpen ? 'Hide the map' : 'Show the map'}
            </Button>
          </Box>
          {mapOpen && (
            <Box sx={{height: 400, zIndex: 1000, display: 'block'}}>
              <MapComponent position={[activity.latitude, activity.longitude]} venue={activity.venue} />
            </Box>
           )}
        </Card>
      );
    }
        