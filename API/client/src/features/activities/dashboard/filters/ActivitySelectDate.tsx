import { Box, Paper, Typography } from '@mui/material'
import {Event} from '@mui/icons-material'
import 'react-calendar/dist/Calendar.css'
import Calendar from 'react-calendar'

export default function ActivitySelectDate() {
  return (
    <Box component={Paper} sx={{width: '100%' , p: 3, borderRadius: 3}}>
        <Typography variant='h6' sx={{display: 'flex' , alignItems: 'center' , mb: 1 , color:'primary.main'}}>
            <Event sx={{mr:1}}></Event>
            Select Date
        </Typography>
        <Calendar />
    </Box>
  )
}
