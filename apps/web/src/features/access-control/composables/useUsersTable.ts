import { 
    getCoreRowModel, 
    getFilteredRowModel,
    useVueTable,
} from "@tanstack/vue-table"
import type { User } from "../types/users"
import { computed, unref, type Ref } from "vue"
import type { InfiniteData } from "@tanstack/vue-query"
import type { Page } from "../types/shared"

export const useUsersTable = (users: Ref<InfiniteData<Page<User>> | undefined>) => {
    const data = computed(() => unref(users.value)?.pages.flatMap(page => page.items) ?? [])
    
    const columns = computed(() => {
        if (!data.value.length) return [];
        return Object.keys(data.value[0]!).map(key => ({
            accessorKey: key,
            header: key.charAt(0).toUpperCase() + key.slice(1)
        }))
    })

    const table = useVueTable({
        get data() {
            return unref(data);
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