import { PersonOff  } from '@mui/icons-material'
import { Paper,Typography,Button } from '@mui/material'
import { Link} from "react-router";

export default function UnauthError() {
  return (
 <Paper 
 sx={{
    display: 'flex',
    flexDirection: 'column',
    justifyContent: 'center',
    alignItems: 'center',
    height: '50vh',
    p: 6
 }}
 >
    <PersonOff  sx={{ fontSize: '100px', color: 'primary.main' }} />
          <Typography gutterBottom variant='h5'>Opps - you are not authorized to reach this page, 
            ask the admin user for permission</Typography>
          <Button component={Link} to='/activities' variant='contained'   fullWidth >Main Page</Button>
 </Paper> 
  )
}
