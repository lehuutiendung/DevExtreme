<template>
  <DxDataGrid
    :data-source="dataSource"
    :remote-operations="false"
    :allow-column-reordering="true"
    :allow-column-resizing="true"
    column-resizing-mode="widget"
    :column-min-width="300"
    :column-auto-width="true"
    :show-borders="true"
    :height="550"
    @content-ready="onContentReady"
    :column-width="300"
    :hover-state-enabled="true"
  >
    
    <DxColumn
      data-field="Amount"
      caption="Tên chính sách"
      data-type="number"
      format="currency"
      alignment="left"
      :fixed="true"
    />
    <DxColumn
      :allow-grouping="false"
      data-field="Discount"
      caption="Nhân viên áp dụng"
      data-type="number"
      format="percent"
      alignment="left"
      cell-template="discountCellTemplate"
      css-class="bullet"
    />
    <DxColumn
      data-field="SaleDate"
      caption="Vị trí áp dụng"
      data-type="date"
    />
    <DxColumn
      data-field="Region"
      caption="Đơn vị áp dụng"
      data-type="string"
    />
    <DxColumn
      data-field="Sector"
      caption="Trạng thái"
      data-type="string"
    />
    
    <DxSearchPanel
      :visible="true"
      :highlight-case-sensitive="false"
      :highlight-search-text="false"
      placeholder="Tìm kiếm"
    />

    <DxGrouping :auto-expand-all="false"/>
    <DxPager
      :allowed-page-sizes="pageSizes"
      :show-page-size-selector="true"
      displayMode="compact"
    />
    <DxPaging :page-size="10" />
    <DxScrolling column-rendering-mode="virtual"/>
    <DxSelection
        :select-all-mode="allMode"
        :show-check-boxes-mode="checkBoxesMode"
        mode="multiple"
      />

    <template #cellTemplate="{ }">
      <div>
          <DxButton icon="edit"/>
      </div>
    </template>
  </DxDataGrid>
</template>
<script>

import {
  DxDataGrid,
  DxColumn,
  DxGrouping,
  DxPager,
  DxPaging,
  DxSearchPanel,
  DxScrolling,
  DxSelection,
} from 'devextreme-vue/data-grid';
import DataSource from 'devextreme/data/data_source';
import 'devextreme/data/odata/store';
import DxButton from 'devextreme-vue/button'; 

let collapsed = false;

export default {
  components: {
    DxDataGrid,
    DxColumn,
    DxGrouping,
    DxPager,
    DxPaging,
    DxSearchPanel,
    DxScrolling,
    DxSelection,
    DxButton,
  },
  data() {
    return {
      dataSource: new DataSource({
        store: {
          type: 'odata',
          url: 'https://js.devexpress.com/Demos/SalesViewer/odata/DaySaleDtoes',
          key: 'Id',
          beforeSend: function(request) {
            request.params.startDate = '2020-05-10';
            request.params.endDate = '2020-05-15';
          }
        }
      }),
      pageSizes: [10, 25, 50, 100],
      onContentReady: function(e) {
        if (!collapsed) {
          e.component.expandRow(['EnviroCare']);
          collapsed = true;
        }
      },
      allMode: 'page',
      checkBoxesMode: 'always',
    };
  },
  methods: {
    
  },
};
</script>

<style scoped>
    @import "../../css/base/base-data-grid.css";
</style>

