import {
  Badge,
  Button,
  Card,
  CardActions,
  CardContent,
  CardMedia,
  Chip,
  Typography,
} from "@mui/material";
import {Link} from 'react-router'
import { FormatDate } from "../../../lib/util/util";
import { Activity } from "../../../lib/types";
type Props = {
    activity : Activity,
   } 
export default function ActivityDetailsHeader({activity}: Props) {
  const isCancelled = false;
  const isHost = true;
  const isGoing = false;
  const loading = false;


  return (
    <Card style={{  position: "relative", color: "#fff" }}>
      <Badge sx={{ position: "absolute", left: 40, top: 20, zIndex: 1000 }} />
      <CardMedia
        component="img"
        alt="Scenic View"
        height="200"
        image={`/images/categoryimages/${activity.categoryName}.jpg`}
        style={{ filter: "brightness(70%)" }}
      />
      {isCancelled && (<Chip
        label="Cancelled"
        color="error"
        style={{
          position: "absolute",
          top: 10,
          left: 10,
          color: "white",
          fontWeight: "bold",
        }}
      />)}
      <CardContent
        style={{
          position: "absolute",
          top: "70%",
          left: "3%",
          transform: "translateY(-50%)",
        }}
      >
        <Typography variant="h5" component="div">
          {activity.title}
        </Typography>
        <Typography variant="body2">{FormatDate(activity.date)}</Typography>
        <Typography variant="body2">Hosted by Bob</Typography>
      </CardContent>

      <CardActions sx={{ position: "absolute", bottom: 10, right: 10 }}>
        {isHost && (<>
            <Button sx={{textTransform:'none'}} 
            variant="contained" 
            color={isCancelled ? "success": "error"} onClick={()=>{}}>
          {isCancelled ? 'Re-activate Activity': 'Cancel Activity'} 
        </Button>
        <Button  sx={{textTransform:'none'}}
          variant="contained"
          color="primary"
          style={{ marginLeft: "10px" }}
          disabled={isCancelled || loading}
          component={Link} to={`/editActivity/${activity.id}`}
        >
          {isGoing ? 'Cancel Activity' : 'Join Axctivity'} 
        </Button></>)
        }
      </CardActions>
    </Card>
  );
}
