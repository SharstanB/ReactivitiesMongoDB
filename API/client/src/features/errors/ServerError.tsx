import {Divider, Paper, Typography } from '@mui/material'
import { useLocation } from 'react-router';
export default function ServerError() {
  const {state} = useLocation();
 return (
<Paper>
    {state.error ? (
        <>
        <Typography gutterBottom  variant='h6' sx={{px: 4, py: 2}} color='secondary'>
            {state?.error.message || 'Internal server error'}</Typography>
        <Divider/>
        <Typography variant='body1' sx={{px: 4, py:3}}>
            {state?.error.ExceptionDetails || 'Internal server error'}</Typography>

        </>
    ) : (
        <Typography variant='body1'>Server Error</Typography>
    )}
</Paper>

)
}
