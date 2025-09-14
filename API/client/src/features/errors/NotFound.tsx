import { SearchOff } from '@mui/icons-material'
import { Paper,Typography,Button } from '@mui/material'
import { Link} from "react-router";

export default function NotFound() {
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
    <SearchOff sx={{ fontSize: '100px', color: 'primary.main' }} />
          <Typography gutterBottom variant='h5'>Opps - we could not find what you are looking for</Typography>
          <Button component={Link} to='/' variant='contained'   fullWidth >Go to home</Button>
 </Paper> 
  )
}
