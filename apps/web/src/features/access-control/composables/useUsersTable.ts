import { 
    getCoreRowModel, 
    getFilteredRowModel,
    useVueTable 
} from "@tanstack/vue-table"
import type { User } from "../types/users"
import { computed, unref, type Ref } from "vue"

export const useUsersTable = (users: Ref<User[]>) => {
    const columns = computed(() => {
        if (!users.value.length) return [];
        return Object.keys(users.value[0]!).map(key => ({
            accessorKey: key,
            header: key.charAt(0).toUpperCase() + key.slice(1)
        }))
    })

    const table = useVueTable({
        get data() {
            return unref(users)
        },
        get columns() {
            return unref(columns);
        },
        getCoreRowModel: getCoreRowModel(),
        getFilteredRowModel: getFilteredRowModel(), // 🔥 REQUIRED
        globalFilterFn: 'includesString', // simple contains filter
    })

    return {
        table
    }
}