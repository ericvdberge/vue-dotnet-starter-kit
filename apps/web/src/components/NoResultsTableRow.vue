
<script setup lang="ts">
import { TableCell, TableRow } from '@/components/ui/table';
</script>

<template>
     <TableRow
        class="flex w-full"
    >
        <TableCell  class="flex h-12 w-full items-center text-sm text-muted-foreground">            
             <div class="flex items-center gap-2">
                <!-- gray indicator -->
                <div class="h-1.5 w-1.5 rounded-full bg-muted-foreground/40"/>
                <span>No results found.</span>
            </div>
        </TableCell>
    </TableRow>
</template>