// import { Typography,Button, ButtonGroup, Box, Paper, ListItem, List } from "@mui/material"
// import { useStore } from "../../lib/hooks/useStore"
// import { observer } from 'mobx-react-lite'
 
// // we can use the Observer to observe only part of the component 
// //  export default function Counter() {
// //     const {counterStore} = useStore();
// //    return (
// //     <>
// //         <Observer>
// //         {() => (
// //             <>
// //             <Typography variant="h4" gutterBottom>{counterStore.title}</Typography>
// //             <Typography variant="h6" gutterBottom>The Count is : {counterStore.count}</Typography>
// //             </>
// //         )
// //         }
// //      </Observer>
// //         <ButtonGroup sx={{mt:3}}>
// //             <Button onClick={() => counterStore.increment()} variant="contained" color="primary">Increment</Button>
// //             <Button onClick={() => counterStore.decrement()} variant="contained" color="error" >Decrement</Button>
// //             <Button onClick={() => counterStore.increment(5)} variant="contained" color="secondary" >Increment by 5</Button>
// //      </ButtonGroup>
    
// //     </>

// // and we can use the Observer to wrapp whole component like in the below example 
// const Counter = observer(function Counter() {
//     const {counterStore} = useStore();
//     return (
//     <Box sx={{display: "flex", justifyContent: "space-between"}}>
//         <Box sx={{width: "60%"}}>
//         <Typography variant="h4" gutterBottom>{counterStore.title}</Typography>
//         <Typography variant="h6" gutterBottom>The Count is : {counterStore.count}</Typography>
//         <ButtonGroup sx={{mt:3}}>
//             <Button onClick={() => counterStore.increment()} variant="contained" color="primary">Increment</Button>
//             <Button onClick={() => counterStore.decrement()} variant="contained" color="error" >Decrement</Button>
//             <Button onClick={() => counterStore.increment(5)} variant="contained" color="secondary" >Increment by 5</Button>
//      </ButtonGroup>
//         </Box>
//         <Paper sx={{width: "40%", p:4}}>
//             <Typography variant="h4">Counter events : {counterStore.countString}</Typography>
//             <List>
//                 {counterStore.events.map((event, index)=> {
//                     return <ListItem key={index}>{event}</ListItem>
//                 })}
//             </List>
//         </Paper>
//     </Box>)
//  });
//  export default Counter;
