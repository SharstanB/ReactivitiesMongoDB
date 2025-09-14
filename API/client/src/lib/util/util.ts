import { DateArg, format } from "date-fns";

export function FormatDate(date: DateArg<Date>) {
  return format(date, "dd MMM yyy h:mm a");
}
