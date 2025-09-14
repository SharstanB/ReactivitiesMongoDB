import { Button, Paper, Typography } from '@mui/material'
import GroupIcon from '@mui/icons-material/Group';
import { Link } from 'react-router';
export default function HomePage() {
  return (
    <Paper
    sx={{
      height: '100vh',
      background: 'linear-gradient(153deg, #182a73 0%, #218aae 69%, #20a7ac 89%)',
      display: 'flex',
      flexDirection: 'column',
      justifyContent: 'center',
      alignItems: 'center',
      color: 'white',
      textAlign: 'center',
    }}
  >
    <GroupIcon sx={{ fontSize: 60, mb: 2 }} />
    <Typography variant="h3" sx={{ fontWeight: 500 }}>
      Reactivities
    </Typography>
    <Typography variant="h5" sx={{ mb: 4, mt: 1 }}>
      Welcome to reactivities
    </Typography>
    <Button
      variant="contained"
      sx={{
        backgroundColor: '#1976d2',
        color: 'white',
        fontWeight: 'bold',
        px: 4,
        py: 1.5,
        borderRadius: '10px',
        boxShadow: 3,
        ':hover': {
          backgroundColor: '#1565c0',
        },
      }}
      component={Link} to="/login"
      // onClick={handleClick}
    >
      TAKE ME TO THE ACTIVITIES!
    </Button>
  </Paper>
  )
}
