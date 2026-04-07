import { getSecureOptions } from "@/generated/@tanstack/vue-query.gen";
import { useQuery } from "@tanstack/vue-query";

export const useSecureKeys = () => {
    return useQuery(getSecureOptions());
};